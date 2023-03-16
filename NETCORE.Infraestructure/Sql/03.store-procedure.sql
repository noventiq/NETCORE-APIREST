CREATE PROCEDURE USP_SELECT_PRODUCTS
AS
BEGIN
	SELECT * FROM Products
END
GO

CREATE PROCEDURE USP_SELECT_PRODUCT_BY_ID(
@id INT
)
AS
BEGIN
	SELECT * FROM Products WHERE id=@id
END
gO


CREATE PROCEDURE USP_USER_LOGIN(
@username VARCHAR(50),
@password VARCHAR(50)
)
AS
BEGIN
	SELECT * FROM users WHERE username=@username and password=@password

	SELECT p.* FROM users u
	INNER JOIN users_profiles up on U.id=up.user_id
	INNER JOIN profiles p on p.id=up.profile_id
	WHERE  u.username=@username and u.password=@password
	
END
go