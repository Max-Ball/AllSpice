
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  picture VARCHAR(255) NOT NULL,
  title VARCHAR(255) NOT NULL,
  subtitle VARCHAR(255) NOT NULL,
  category VARCHAR(255) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY(creatorId) REFERENCES accounts(id)
)default charset utf8 COMMENT '';

INSERT INTO recipes
(picture, title, subtitle, category, creatorId)
VALUES
('https://images.unsplash.com/photo-1567620905732-2d1ec7ab7445?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8Zm9vZHxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=500&q=60', 'Pancakes', 'All you need is flour and water', 'Breakfast', '63237c712d0c7a123937383b');

SELECT * FROM recipes r
JOIN accounts a ON a.id = r.creatorId;

CREATE TABLE IF NOT EXISTS ingredients(
  id INT AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,

  FOREIGN KEY(recipeId) REFERENCES recipes(id)
)default charset utf8 COMMENT '';

INSERT INTO ingredients
(name, quantity, recipeId)
VALUES
('Butter', '5 sticks', 1);

INSERT INTO ingredients
(name, quantity, recipeId)
VALUES
('Water', '2 gallons', 1);

INSERT INTO ingredients
(name, quantity, recipeId)
VALUES
('Flour', 'a whole bag', 1);

SELECT * FROM ingredients i
JOIN recipes r ON r.id = i.recipeId;

CREATE TABLE IF NOT EXISTS instructions(
  id INT AUTO_INCREMENT PRIMARY KEY,
  step INT NOT NULL,
  body VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL,

  FOREIGN KEY(recipeId) REFERENCES recipes(id)
)default charset utf8 COMMENT '';

INSERT INTO instructions
(step, body, recipeId)
VALUES
(1, 'Grab a big bowl and mix the flour and water thoroughly', 1);

INSERT INTO instructions
(step, body, recipeId)
VALUES
(2, 'Heat up a large pan on medium heat and pour mixture into pan', 1);

INSERT INTO instructions
(step, body, recipeId)
VALUES
(3, 'Cook for 5 minutes and flip over. Cook for another 5 minutes', 1);

INSERT INTO instructions
(step, body, recipeId)
VALUES
(4, 'Pull from heat and plate with butter on top.', 1);

SELECT * FROM instructions i
JOIN recipes r ON r.id = i.recipeId;

CREATE TABLE IF NOT EXISTS favorites(
id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
recipeId INT NOT NULL,
profileId VARCHAR(255) NOT NULL,
FOREIGN KEY (recipeId) REFERENCES recipes(id),
FOREIGN KEY (profileId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

INSERT INTO favorites
(recipeId, profileId)
VALUES
(1, '63237c712d0c7a123937383b');


SELECT
f.*,
r.*,
a.*
FROM favorites f
JOIN recipes r ON f.recipeId = r.id
JOIN accounts a ON r.creatorId = a.id
WHERE f.profileId = '63237c712d0c7a123937383b';