
INSERT INTO Users (usr_id, username, email, password, admin, borrowed_books, penalty) VALUES
(123, "karel", "karel@seznam.cz", "heslo", 1, 0, 0),
(345, "pepa", "pepa_novak@gmail.com", "heslo", 0, 0, 0),
(456, "petr", "petr.sasek.cz", "heslo", 0, 0, 0);

    
INSERT INTO Books (id, isbn, title, author, year, genre, copies, available) VALUES
(1, '9780743273565', 'The Great Gatsby', 'F. Scott Fitzgerald', 1925, 'Classic', 85, 42),
(2, '9780061120084', 'To Kill a Mockingbird', 'Harper Lee', 1960, 'Classic', 91, 28),
(3, '9780451524935', '1984', 'George Orwell', 1949, 'Dystopian', 73, 15),
(4, '9780547928227', 'The Hobbit', 'J.R.R. Tolkien', 1937, 'Fantasy', 96, 31),
(5, '9780618640157', 'The Lord of the Rings', 'J.R.R. Tolkien', 1954, 'Fantasy', 100, 24),
(6, '9780439554930', 'Harry Potter and the Sorcerer''s Stone', 'J.K. Rowling', 1997, 'Fantasy', 98, 12),
(7, '9780439064873', 'Harry Potter and the Chamber of Secrets', 'J.K. Rowling', 1998, 'Fantasy', 87, 26),
(8, '9780439136365', 'Harry Potter and the Prisoner of Azkaban', 'J.K. Rowling', 1999, 'Fantasy', 81, 18),
(9, '9780439139601', 'Harry Potter and the Goblet of Fire', 'J.K. Rowling', 2000, 'Fantasy', 94, 22),
(10, '9780439358071', 'Harry Potter and the Order of the Phoenix', 'J.K. Rowling', 2003, 'Fantasy', 89, 11),

(11, '9780307474278', 'The Da Vinci Code', 'Dan Brown', 2003, 'Thriller', 67, 29),
(12, '9780307743657', 'Angels & Demons', 'Dan Brown', 2000, 'Thriller', 72, 21),
(13, '9780385504201', 'The Kite Runner', 'Khaled Hosseini', 2003, 'Drama', 63, 19),
(14, '9781594631931', 'The Alchemist', 'Paulo Coelho', 1988, 'Fiction', 90, 34),
(15, '9780141439518', 'Pride and Prejudice', 'Jane Austen', 1813, 'Romance', 58, 25),
(16, '9780140449136', 'The Odyssey', 'Homer', -700, 'Epic', 44, 17),
(17, '9780140449266', 'The Iliad', 'Homer', -750, 'Epic', 38, 9),
(18, '9780060850524', 'Brave New World', 'Aldous Huxley', 1932, 'Dystopian', 75, 30),
(19, '9780140283334', 'Animal Farm', 'George Orwell', 1945, 'Satire', 80, 14),
(20, '9780316769488', 'The Catcher in the Rye', 'J.D. Salinger', 1951, 'Classic', 65, 20),

(21, '9780553296983', 'Dune', 'Frank Herbert', 1965, 'Science Fiction', 92, 33),
(22, '9780345339683', 'Foundation', 'Isaac Asimov', 1951, 'Science Fiction', 51, 12),
(23, '9780441172719', 'Dune Messiah', 'Frank Herbert', 1969, 'Science Fiction', 46, 18),
(24, '9780385732550', 'The Book Thief', 'Markus Zusak', 2005, 'Historical Fiction', 84, 35),
(25, '9781400033416', 'Life of Pi', 'Yann Martel', 2001, 'Adventure', 57, 22),
(26, '9780307387899', 'The Road', 'Cormac McCarthy', 2006, 'Post-Apocalyptic', 62, 16),
(27, '9780385490818', 'Fight Club', 'Chuck Palahniuk', 1996, 'Drama', 54, 13),
(28, '9780062315007', 'Sapiens', 'Yuval Noah Harari', 2011, 'History', 88, 41),
(29, '9780804139021', 'Educated', 'Tara Westover', 2018, 'Memoir', 47, 19),
(30, '9780385545969', 'Project Hail Mary', 'Andy Weir', 2021, 'Science Fiction', 79, 28);