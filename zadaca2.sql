CREATE TABLE products (
    product_id INT PRIMARY KEY NOT NULL,
    product_name VARCHAR(255) NOT NULL,
    product_price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE employees (
    employee_id INT PRIMARY KEY NOT NULL,
    employee_name VARCHAR(255) NOT NULL
);

CREATE TABLE orders (
    order_id INT PRIMARY KEY NOT NULL,
    delivery_date DATE NOT NULL,
    employee_id INT NOT NULL,
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
);

CREATE TABLE order_items (
    order_item_id INT PRIMARY KEY NOT NULL,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);

INSERT INTO products (product_id, product_name, product_price) VALUES
(1, 'Product1', 10),
(2, 'Product2', 15.50),
(3, 'Product3', 25);

INSERT INTO employees (employee_id, employee_name) VALUES
(1, 'Petko Petkov'),
(2, 'Trajko Trajkov');

INSERT INTO orders (order_id, delivery_date, employee_id) VALUES
(1, '2024-04-01', 1),
(2, '2024-04-02', 1),
(3, '2024-04-03', 2),
(4, '2024-03-03', 2),
(5, '2024-02-05', 1);

INSERT INTO order_items (order_item_id, order_id, product_id) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 3),
(4, 3, 1),
(5, 3, 2),
(6, 4, 2),
(7, 5, 3);

-- Write a query that returns the total amount that a given employee has earned in a given period.

SELECT e.employee_name, SUM(p.product_price) AS total_earned
FROM employees e
JOIN orders o ON e.employee_id = o.employee_id
JOIN order_items oi ON o.order_id = oi.order_id
JOIN products p ON oi.product_id = p.product_id
GROUP BY e.employee_name;

-- Write a query that returns the total amount earned by month.

SELECT YEAR(o.delivery_date) AS year,
       MONTH(o.delivery_date) AS month,
       SUM(p.product_price) AS total_earned
FROM orders o
JOIN order_items oi ON o.order_id = oi.order_id
JOIN products p ON oi.product_id = p.product_id
GROUP BY YEAR(o.delivery_date), MONTH(o.delivery_date);