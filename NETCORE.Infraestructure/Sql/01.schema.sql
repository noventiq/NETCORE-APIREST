create table Products(
 id int PRIMARY KEY IDENTITY(1,1),
 title varchar(500),
 description varchar(1024),
 price decimal(10,2),
 discountPercentage decimal(10,2),
 rating decimal(10,2),
 stock int,
 brand varchar(50),
 category varchar(50),
 createdAt datetime,
 createdBy varchar(50),
 updatedAt datetime,
 updatedBy varchar(50)
)


CREATE TABLE ProductThumbnails(
 id int PRIMARY KEY IDENTITY(1,1),
 products_id INT,
 url varchar(1024),
 createdAt datetime,
 createdBy varchar(50),
 updatedAt datetime,
 updatedBy varchar(50)
);

CREATE TABLE carts (
    [id] INT,
    [products_id] INT,
    [products_title] NVARCHAR(45),
    [products_price] INT,
    [products_quantity] INT,
    [products_total] INT,
    [products_discountPercentage] NUMERIC(4, 2),
    [products_discountedPrice] INT,
    [total] INT,
    [discountedTotal] INT,
    [userId] INT,
    [totalProducts] INT,
    [totalQuantity] INT
);

create table users(
id INT PRIMARY KEY,
username varchar(100),
password varchar(100),
email varchar(100)
);

create table profiles(
id INT primary key,
name varchar(100)
);

create table users_profiles (
user_id INT,
profile_id INT
PRIMARY KEY (user_id,profile_id),
CONSTRAINT FK_users_profiles_user_id FOREIGN KEY (user_id)
        REFERENCES dbo.users (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
CONSTRAINT FK_users_profiles_profile_id FOREIGN KEY (profile_id)
        REFERENCES dbo.profiles (id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);