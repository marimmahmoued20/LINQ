
using System;
using System.Collections.Generic;
using System.Linq;

#region Library Classes

class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int CopiesInStock { get; set; }
}

class Loan
{
    public int LoanId { get; set; }
    public string BookTitle { get; set; }

    public override string ToString()
    {
        return $"Loan ID: {LoanId}, Book: {BookTitle}";
    }
}

class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public List<Loan> Loans { get; set; } = new List<Loan>();
}

#endregion

class Program
{
    static void Main()
    {
        #region Library Data

        List<Book> BookList = new List<Book>
        {
            new Book { BookId = 1, Title = "C# Basics", Genre = "Programming", CopiesInStock = 5 },
            new Book { BookId = 2, Title = "Clean Code", Genre = "Programming", CopiesInStock = 0 },
            new Book { BookId = 3, Title = "Database Systems", Genre = "Science", CopiesInStock = 3 },
            new Book { BookId = 4, Title = "Computer Science", Genre = "Programming", CopiesInStock = 2 },
            new Book { BookId = 5, Title = "History of Egypt", Genre = "History", CopiesInStock = 4 }
        };

        List<Member> MemberList = new List<Member>
        {
            new Member
            {
                MemberId = 1,
                Name = "Ahmed",
                Loans = new List<Loan>
                {
                    new Loan { LoanId = 101, BookTitle = "C# Basics" },
                    new Loan { LoanId = 102, BookTitle = "Clean Code" }
                }
            },
            new Member
            {
                MemberId = 2,
                Name = "Sara",
                Loans = new List<Loan>
                {
                    new Loan { LoanId = 103, BookTitle = "Database Systems" }
                }
            },
            new Member
            {
                MemberId = 3,
                Name = "Ali",
                Loans = new List<Loan>
                {
                    new Loan { LoanId = 104, BookTitle = "Computer Science" }
                }
            }
        };

        #endregion

        #region Part 1: Filtration (Where)

        Console.WriteLine("\n========== Part 1: Filtration ==========");

        // Q1: Get books whose CopiesInStock is greater than 0.

        Console.WriteLine("\nQ1 - Fluent Syntax:");

        var q1 = BookList.Where(book => book.CopiesInStock > 0);

        foreach (var book in q1)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nQ1 - Query Syntax:");

        var q1Query =
            from book in BookList
            where book.CopiesInStock > 0
            select book;

        foreach (var book in q1Query)
            Console.WriteLine(book.Title);


        // Q2: Get Programming books that are currently in stock.

        Console.WriteLine("\nQ2 - Fluent Syntax:");

        var q2 = BookList.Where(book =>
            book.Genre == "Programming" && book.CopiesInStock > 0);

        foreach (var book in q2)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nQ2 - Query Syntax:");

        var q2Query =
            from book in BookList
            where book.Genre == "Programming" && book.CopiesInStock > 0
            select book;

        foreach (var book in q2Query)
            Console.WriteLine(book.Title);


        // Q3: Get books whose titles start with C.

        Console.WriteLine("\nQ3 - Fluent Syntax:");

        var q3 = BookList.Where(book => book.Title.StartsWith("C"));

        foreach (var book in q3)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nQ3 - Query Syntax:");

        var q3Query =
            from book in BookList
            where book.Title.StartsWith("C")
            select book;

        foreach (var book in q3Query)
            Console.WriteLine(book.Title);

        #endregion

        #region Part 2: Projection (Select)

        Console.WriteLine("\n========== Part 2: Projection ==========");

        // Q4: Print only book titles.

        Console.WriteLine("\nQ4 - Fluent Syntax:");

        var q4 = BookList.Select(book => book.Title);

        foreach (var title in q4)
            Console.WriteLine(title);

        // Query Syntax
        Console.WriteLine("\nQ4 - Query Syntax:");

        var q4Query =
            from book in BookList
            select book.Title;

        foreach (var title in q4Query)
            Console.WriteLine(title);


        // Q5: Print BookId and Title using anonymous type.

        Console.WriteLine("\nQ5 - Fluent Syntax:");

        var q5 = BookList.Select(book => new
        {
            book.BookId,
            book.Title
        });

        foreach (var book in q5)
            Console.WriteLine($"{book.BookId} - {book.Title}");

        // Query Syntax
        Console.WriteLine("\nQ5 - Query Syntax:");

        var q5Query =
            from book in BookList
            select new
            {
                book.BookId,
                book.Title
            };

        foreach (var book in q5Query)
            Console.WriteLine($"{book.BookId} - {book.Title}");


        // Q6: Format each book using string interpolation.

        Console.WriteLine("\nQ6 - Fluent Syntax:");

        var q6 = BookList.Select(book =>
            $"[{book.BookId}] {book.Title} - {book.Genre}");

        foreach (var book in q6)
            Console.WriteLine(book);

        // Query Syntax
        Console.WriteLine("\nQ6 - Query Syntax:");

        var q6Query =
            from book in BookList
            select $"[{book.BookId}] {book.Title} - {book.Genre}";

        foreach (var book in q6Query)
            Console.WriteLine(book);

        #endregion

        #region Part 3: SelectMany

        Console.WriteLine("\n========== Part 3: SelectMany ==========");

        // Q7: Get all loans of all members as one sequence.

        Console.WriteLine("\nQ7 - Fluent Syntax:");

        var q7 = MemberList.SelectMany(member => member.Loans);

        foreach (var loan in q7)
            Console.WriteLine(loan);

        // Query Syntax
        Console.WriteLine("\nQ7 - Query Syntax:");

        var q7Query =
            from member in MemberList
            from loan in member.Loans
            select loan;

        foreach (var loan in q7Query)
            Console.WriteLine(loan);


        // Q8: Filter members first, then get their loans.

        Console.WriteLine("\nQ8 - Fluent Syntax:");

        var q8 = MemberList
            .Where(member => member.Name.StartsWith("A"))
            .SelectMany(member => member.Loans);

        foreach (var loan in q8)
            Console.WriteLine(loan);

        // Query Syntax
        Console.WriteLine("\nQ8 - Query Syntax:");

        var q8Query =
            from member in MemberList
            where member.Name.StartsWith("A")
            from loan in member.Loans
            select loan;

        foreach (var loan in q8Query)
            Console.WriteLine(loan);


        // Q9: Use Select instead of SelectMany.
        // Select returns IEnumerable<IEnumerable<Loan>>,
        // while SelectMany returns IEnumerable<Loan>.
        // Select keeps nested collections, but SelectMany flattens them.

        Console.WriteLine("\nQ9 - Fluent Syntax:");

        var q9 = MemberList.Select(member => member.Loans);

        foreach (var memberLoans in q9)
        {
            foreach (var loan in memberLoans)
                Console.WriteLine(loan);
        }

        // Query Syntax
        Console.WriteLine("\nQ9 - Query Syntax:");

        var q9Query =
            from member in MemberList
            select member.Loans;

        foreach (var memberLoans in q9Query)
        {
            foreach (var loan in memberLoans)
                Console.WriteLine(loan);
        }

        #endregion

        #region Part 4: Execution Types

        Console.WriteLine("\n========== Part 4: Execution Types ==========");

        // Q10: Deferred Execution.
        // The query executes when foreach starts.
        // Therefore, a newly added book appears in the result.

        Console.WriteLine("\nQ10 - Fluent Syntax:");

        var q10 = BookList.Where(book => book.CopiesInStock > 0);

        BookList.Add(new Book
        {
            BookId = 6,
            Title = "LINQ Guide",
            Genre = "Programming",
            CopiesInStock = 3
        });

        foreach (var book in q10)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nQ10 - Query Syntax:");

        var q10Query =
            from book in BookList
            where book.CopiesInStock > 0
            select book;

        BookList.Add(new Book
        {
            BookId = 7,
            Title = "C Sharp Advanced",
            Genre = "Programming",
            CopiesInStock = 2
        });

        foreach (var book in q10Query)
            Console.WriteLine(book.Title);


        // Q11: Eager Execution using ToList().
        // ToList executes the query immediately.
        // Newly added books will not appear in the saved result.

        Console.WriteLine("\nQ11 - Fluent Syntax:");

        var q11 = BookList
            .Where(book => book.CopiesInStock > 0)
            .ToList();

        BookList.Add(new Book
        {
            BookId = 8,
            Title = "Python Basics",
            Genre = "Programming",
            CopiesInStock = 4
        });

        foreach (var book in q11)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nQ11 - Query Syntax:");

        var q11Query =
            (from book in BookList
             where book.CopiesInStock > 0
             select book).ToList();

        BookList.Add(new Book
        {
            BookId = 9,
            Title = "Java Guide",
            Genre = "Programming",
            CopiesInStock = 5
        });

        foreach (var book in q11Query)
            Console.WriteLine(book.Title);

        #endregion

        #region Bonus

        Console.WriteLine("\n========== Bonus ==========");

        // Bonus 1: Combine conditions using OR and AND.

        Console.WriteLine("\nBonus 1 - Fluent Syntax:");

        var bonus1 = BookList.Where(book =>
            (book.Genre == "Programming" || book.Genre == "Science")
            && book.CopiesInStock > 0);

        foreach (var book in bonus1)
            Console.WriteLine(book.Title);

        // Query Syntax
        Console.WriteLine("\nBonus 1 - Query Syntax:");

        var bonus1Query =
            from book in BookList
            where (book.Genre == "Programming" || book.Genre == "Science")
                  && book.CopiesInStock > 0
            select book;

        foreach (var book in bonus1Query)
            Console.WriteLine(book.Title);


        // Bonus 2: Select with index.

        Console.WriteLine("\nBonus 2 - Fluent Syntax:");

        var bonus2 = BookList.Select((book, index) => new
        {
            index,
            book.Title
        });

        foreach (var book in bonus2)
            Console.WriteLine($"{book.index} - {book.Title}");

        // Query Syntax
        Console.WriteLine("\nBonus 2 - Query Syntax:");

        var bonus2Query =
            from book in BookList.Select((book, index) => new
            {
                index,
                book.Title
            })
            select book;

        foreach (var book in bonus2Query)
            Console.WriteLine($"{book.index} - {book.Title}");

        #endregion

        Console.WriteLine("\n========== Program Finished ==========");
    }
}
