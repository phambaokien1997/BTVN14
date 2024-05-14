CREATE DATABASE BTVN14
GO
USE BTVN14
GO
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100),
    Quantity INT,
    ExpirationDate DATE
);
CREATE PROCEDURE AddProduct
    @ProductName NVARCHAR(100),
    @Quantity INT,
    @ExpirationDate DATE
AS
BEGIN
    INSERT INTO Products (ProductName, Quantity, ExpirationDate)
    VALUES (@ProductName, @Quantity, @ExpirationDate)
END
CREATE PROCEDURE CheckInventory
    @ProductName NVARCHAR(100)
AS
BEGIN
    SELECT ProductName, Quantity, ExpirationDate
    FROM Products
    WHERE ProductName = @ProductName
END