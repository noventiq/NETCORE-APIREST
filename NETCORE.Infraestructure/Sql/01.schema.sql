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
)