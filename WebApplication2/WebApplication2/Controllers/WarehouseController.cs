using System.Data.SqlClient;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly string connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;";
    [HttpPost]
    public IActionResult AddProductToWarehouse([FromBody] ProductWarehouseRequest request)
    {
        if (request.Amount <= 0)
        {
            return BadRequest("Amount has to be greater than zero");
        }

        using var connection = new SqlConnection(connectionString);
        connection.Open();
        var transaction = connection.BeginTransaction();

        try
        {
            if (!CheckId(connection,
                    "SELECT COUNT(*) FROM Product WHERE IdProduct = @Id", 
                    request.ProductId, transaction))
            {
                transaction.Rollback();
                return NotFound("no product with such ID");
            }
            if (!CheckId(connection, 
                    "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @Id", 
                    request.WarehouseId, transaction))
            {
                transaction.Rollback();
                return NotFound("no product with such ID");
            }

            if (!CheckOrder(connection, 
                    "SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct AND Amount >= @Amount AND CreatedAt < @CreatedAt", 
                    request.ProductId, 
                    request.Amount, 
                    request.CreatedAt,
                    transaction))
            {
                transaction.Rollback();
                return NotFound("problem with Order");
            }

            int? idOrder = GetIdOrder(connection,"SELECT TOP 1 IdOrder " +
                                                 "FROM [Order] " +
                                                 "WHERE IdProduct = @ProductId " +
                                                 "AND Amount >= @Amount " +
                                                 "AND CreatedAt < @CreatedAt " +
                                                 "AND FulfilledAt IS NULL " +
                                                 "ORDER BY CreatedAt DESC",
                request.ProductId, 
                request.Amount, 
                request.CreatedAt,
                transaction);
            if (!idOrder.HasValue)
            {
                transaction.Rollback();
                return BadRequest("No Order found");
            }

            if (CheckCompletedAndId(connection, 
                    "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder", 
                     idOrder.Value,
                    transaction))
            {
                transaction.Rollback();
                return BadRequest("order completed");
            }
            
            
            UpdateFullfilledAt(connection, idOrder.Value, transaction);
            
            InsertWarehouse(connection,transaction,
                            request.ProductId,
                            request.WarehouseId, 
                            idOrder.Value,
                            request.Amount,
                            request.CreatedAt);
            int id = GetLatestIdProductWarehouseByProductId(connection, transaction, request.ProductId);
            transaction.Commit();
            return Ok("Success, id of new record: " + id);
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            return StatusCode(500, $"An error occured: {ex.Message}");
        }
    }
    [HttpPost("AddUsingProcedure")]
    public IActionResult AddProductToWarehouseUsingProcedure([FromBody] ProductWarehouseRequest request)
    {
        if (request.Amount <= 0)
        {
            return BadRequest("Amount must be greater than zero.");
        }

        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = new SqlCommand("AddProductToWarehouse", connection);
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@IdProduct", request.ProductId);
        command.Parameters.AddWithValue("@IdWarehouse", request.WarehouseId);
        command.Parameters.AddWithValue("@Amount", request.Amount);
        command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);
        try
        {
            int id = Convert.ToInt32(command.ExecuteScalar());
            return Ok(new { Message = "success", IdProductWarehouse = id });
                
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }


    
    
    
    
    private bool CheckId(SqlConnection connection, string query, int id, SqlTransaction transaction)
    {
        using SqlCommand command = new SqlCommand(query, connection);
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@Id", id);
        return (int)command.ExecuteScalar() > 0;
    }

    private bool CheckOrder(SqlConnection connection, string query, int id, int amount, DateTime createdAt, SqlTransaction transaction)
    {
        using SqlCommand command = new SqlCommand(query, connection);
        command.Transaction = transaction;
        
        command.Parameters.AddWithValue("@IdProduct", id);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);
        return (int)command.ExecuteScalar() > 0;
    }

    private int? GetIdOrder(SqlConnection connection,string query, int id, int amount, DateTime createdAt, SqlTransaction transaction)
    {
        using var command = new SqlCommand(query, connection);
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@ProductId", id);
        command.Parameters.AddWithValue("@Amount",amount);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);
        var result = command.ExecuteScalar();
        return (int?)result;
    }

    private bool CheckCompletedAndId(SqlConnection connection, string query, int id, SqlTransaction transaction)
    {
        using var command = new SqlCommand(query, connection);
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@IdOrder", id);
        return (int)command.ExecuteScalar() > 0;
        
    }

    private void UpdateFullfilledAt(SqlConnection connection, int id, SqlTransaction transaction)
    {
        string query =  "UPDATE [Order] SET FulfilledAt = GETDATE() WHERE IdOrder = @IdOrder";
        using SqlCommand command = new SqlCommand(query, connection);
        command.Transaction = transaction;
        command.Parameters.AddWithValue("@IdOrder", id);
        command.ExecuteScalar();
    }

    private decimal GetProductPriceByIdProduct(SqlConnection connection, SqlTransaction transaction, int id)
    {
        string query = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
        using SqlCommand command = new SqlCommand(query, connection, transaction);
        command.Parameters.AddWithValue("@IdProduct", id);
        var result = command.ExecuteScalar();
        return Convert.ToDecimal(result);
    }

    private void InsertWarehouse(SqlConnection connection, SqlTransaction transaction, int idProduct, int idWarehouse,int idOrder, int amount, DateTime createdAt)
    {
        decimal price = GetProductPriceByIdProduct(connection, transaction, idProduct)*amount;
        string query = "INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) " +
                       "VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt)";
        using SqlCommand command = new SqlCommand(query, connection, transaction);
        command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@IdOrder", idOrder);
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@Price", price);
        command.Parameters.AddWithValue("@CreatedAt", createdAt);
        command.ExecuteScalar();
    }

    private int GetLatestIdProductWarehouseByProductId(SqlConnection connection, SqlTransaction transaction, int idProduct)
    {
        string query = 
        "SELECT TOP 1 IdProductWarehouse " +
        "FROM Product_Warehouse "+
        "WHERE IdProduct = @IdProduct "+
        "ORDER BY CreatedAt DESC ";

        using SqlCommand command = new SqlCommand(query, connection, transaction );
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        var result = command.ExecuteScalar(); 
        return Convert.ToInt32(result);
    }
}