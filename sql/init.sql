
CREATE TABLE IF NOT EXISTS Books (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    isbn VARCHAR(13) NOT NULL UNIQUE,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    year INTEGER NOT NULL,
    genre VARCHAR(100) NOT NULL,
    copies INTEGER NOT NULL CHECK (copies BETWEEN 1 AND 100),
    available INTEGER NOT NULL CHECK (available >= 0 AND available <= copies)
);


CREATE TABLE IF NOT EXISTS Users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    user_id INTEGER NOT NULL UNIQUE,
    username TEXT NOT NULL UNIQUE,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    admin INTEGER NOT NULL,
    borrowed_books INTEGER NOT NULL,
    penalty INTEGER NOT NULL
);

CREATE TABLE IF NOT EXISTS Borrowed (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    book_id INTEGER NOT NULL,
    user_id INTEGER NOT NULL,
    borrow_date TEXT,
    return_date TEXT,
    return_deadline TEXT
);
