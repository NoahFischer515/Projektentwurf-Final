Module Program

    'User information read from a CSV file
    'Each user entry contains an ID and a name and is separated by "|".
    'Dim Users() As String = Files.ReadAllLines("C:\Users\User\OneDrive\Dokumente\Informatik\library_users.csv")

    Dim userRaw As String =
        "U001,Max Johnson|" &
        "U002,Emily Smith|" &
        "U003,Daniel Brown|" &
        "U004,Laura Wilson|" &
        "U005,Michael Taylor|" &
        "U006,Sarah Anderson|" &
        "U007,James Miller|" &
        "U008,Anna Davis|" &
        "U009,Robert Clark|" &
        "U010,Linda Moore|" &
        "U011,Thomas Martin|" &
        "U012,Jessica Thompson|" &
        "U013,Kevin White|" &
        "U014,Rachel Harris|" &
        "U015,Steven Lewis"

    'The string is converted into an array so each user can be accessed individually
    Dim Users() As String = userRaw.Split("|"c)



    'Book information from CSV file
    'Dim Books() As String = Files.ReadAllLines("C:\Users\User\OneDrive\Dokumente\Informatik\library_books.csv")

    'Book format in this program:
    'ISBN,Title,Author,Status
    'If a book is borrowed, it becomes:
    'ISBN,Title,Author,Status,UserID
    Dim libraryRaw As String =
        "978-0-13-110362-7,Introduction to Programming,John Smith,available|" &
        "978-0-201-03801-9,Data Structures Basics,Alice Brown,available|" &
        "978-0-262-03384-8,Algorithms Explained,Thomas White,available|" &
        "978-0-321-48681-3,Software Engineering Fundamentals,Emily Johnson,available|" &
        "978-1-491-94600-8,Learning VB.NET,Michael Green,available|" &
        "978-0-596-52068-7,Clean Code,Robert Martin,available|" &
        "978-0-13-235088-4,Agile Development,James Wilson,available|" &
        "978-1-59327-584-6,Programming Logic,Sarah Miller,available|" &
        "978-0-201-70073-2,Computer Systems,David Lee,available|" &
        "978-0-321-12742-6,Object-Oriented Design,Laura Clark,available|" &
        "978-0-07-352332-3,Engineering Mathematics,Peter Adams,available|" &
        "978-0-262-16209-8,Discrete Mathematics,Brian Scott,available|" &
        "978-1-118-09387-9,Introduction to Databases,Kevin Turner,available|" &
        "978-0-596-15806-4,Operating Systems Concepts,Nancy Hall,available|" &
        "978-0-13-468599-1,Modern Software Testing,Richard Young,available|" &
        "978-1-4842-0077-9,Beginning Algorithms,Steven King,available|" &
        "978-0-321-35668-0,System Analysis and Design,Angela Moore,available|" &
        "978-0-07-337622-6,Technical Communication,Mark Taylor,available|" &
        "978-1-491-94729-6,Programming Basics,Rachel Evans,available|" &
        "978-0-13-708107-3,Introduction to Networks,Daniel Harris,available|" &
        "978-0-262-53205-1,Artificial Intelligence Basics,Helen Brooks,available|" &
        "978-1-59327-282-1,Problem Solving with Computers,Chris Baker,available|" &
        "978-0-596-51774-8,Linux Fundamentals,Paul Walker,available|" &
        "978-0-13-187325-4,Computer Architecture,Andrew Collins,available|" &
        "978-1-491-94618-3,Programming in Practice,Olivia Reed,available|" &
        "978-0-321-99278-8,Human Computer Interaction,Jason Turner,available|" &
        "978-0-07-180855-2,Information Systems,Rebecca Lewis,available|" &
        "978-1-59327-599-0,Software Development Tools,Matthew Perez,available|" &
        "978-0-596-52067-0,Coding Standards,Benjamin Foster,available|" &
        "978-0-13-117705-5,Fundamentals of Computing,Sophia Anderson,available|"

    'The book list is also split into an array to allow searching and editing
    Dim Libary() As String = libraryRaw.Split("|"c)



    Sub Main()

        Dim selection As Integer

        'Main menu loop.
        'The menu will repeat until the user selects the exit option.
        Do

            Console.Clear()
            Console.WriteLine("Please choose one of the options")
            Console.WriteLine("(1) New user")
            Console.WriteLine("(2) Book variety")
            Console.WriteLine("(3) All Users")
            Console.WriteLine("(4) Borrow a book (ISBN)")
            Console.WriteLine("(5) Show borrowed books (User ID)")
            Console.WriteLine("(6) Give Back (ISBN)")
            Console.WriteLine("(7) Close")
            Console.Write("input: ")

            Dim input As String = Console.ReadLine()

            'Check if the entered value can be converted into a number
            If Not Integer.TryParse(input, selection) Then
                Console.WriteLine("Invalid input. Please enter a number between 1 and 7.")
                Pause()
                Continue Do
            End If


            Select Case selection

                Case 1
                    AddUser()

                Case 2
                    'Print all books stored in the library array
                    PrintArray(Libary)

                Case 3
                    'Print all registered users
                    PrintArray(Users)

                Case 4
                    BorrowBook()

                Case 5
                    ShowBorrowedBooks()

                Case 6
                    Console.WriteLine("input was 6")

                Case 7
                    Console.WriteLine("Goodbye!")
                    Exit Sub

                Case Else
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 7.")

            End Select


            If selection <> 7 Then Pause()

        Loop While selection <> 7

    End Sub



    'Outputs every entry of an array line by line
    Sub PrintArray(ByVal arr() As String)

        For i As Integer = 0 To arr.Length - 1
            Console.WriteLine(arr(i))
        Next

    End Sub



    'Pause used after actions so the user has time to read the output
    Sub Pause()

        Console.WriteLine("Press any key to go back to the menu...")
        Console.ReadKey()

    End Sub



    'Creates a new user and assigns a unique ID
    Sub AddUser()

        'Prevent creating more than 999 users
        If Users.Length >= 999 Then
            Console.WriteLine("Too many users. Please contact the library staff.")
            Return
        End If

        Console.Write("First name: ")
        Dim first As String = Console.ReadLine().Trim()

        Console.Write("Last name: ")
        Dim last As String = Console.ReadLine().Trim()

        'Basic validation to ensure both fields contain text
        If first = "" OrElse last = "" Then
            Console.WriteLine("First or last name cannot be empty.")
            Return
        End If


        'Search the list of users to determine the highest existing ID number
        Dim maxNum As Integer = 0

        For i As Integer = 0 To Users.Length - 1

            Dim parts() As String = Users(i).Split(","c)

            If parts.Length > 0 AndAlso parts(0).StartsWith("U") Then

                Dim n As Integer
                If Integer.TryParse(parts(0).Substring(1), n) Then
                    If n > maxNum Then maxNum = n
                End If

            End If

        Next


        'Create the new ID in format U###
        Dim newId As String = "U" & (maxNum + 1).ToString("D3")
        Dim entry As String = newId & "," & first & " " & last

        userRaw &= "|" & entry
        Users = userRaw.Split("|"c)

        Console.WriteLine("Added New User: " & entry)

    End Sub



    'Handles the process of borrowing a book
    Sub BorrowBook()

        Console.Write("ISBN: ")
        Dim isbn As String = Console.ReadLine()

        Console.Write("User ID (Input with U): ")
        Dim userId As String = Console.ReadLine()


        'Search for the user
        Dim userName As String = ""

        For i As Integer = 0 To Users.Length - 1

            Dim parts() As String = Users(i).Split(","c)

            If parts.Length > 1 AndAlso parts(0) = userId Then
                userName = parts(1)
                Exit For
            End If

        Next

        If userName = "" Then
            Console.WriteLine("User ID not found.")
            Return
        End If


        'Search the book by ISBN
        Dim bookIndex As Integer = -1

        For i As Integer = 0 To Libary.Length - 1

            Dim parts() As String = Libary(i).Split(","c)

            If parts.Length >= 4 AndAlso parts(0) = isbn Then
                bookIndex = i
                Exit For
            End If

        Next

        If bookIndex = -1 Then
            Console.WriteLine("ISBN not found in library.")
            Return
        End If


        Dim bookParts() As String = Libary(bookIndex).Split(","c)

        If bookParts.Length < 4 Then
            Console.WriteLine("Invalid book record.")
            Return
        End If


        If bookParts(3).ToLower() <> "available" Then
            Console.WriteLine("Book is not available for lending.")
            Return
        End If


        'Update book status and store the user who borrowed it
        bookParts(3) = "lend"
        Libary(bookIndex) = bookParts(0) & "," & bookParts(1) & "," & bookParts(2) & "," & bookParts(3) & "," & userId

        Console.WriteLine("Book '" & bookParts(1) & "' (ISBN " & isbn & ") has been lent to " & userName & " (" & userId & ").")

    End Sub



    'Shows all books that are currently borrowed by a specific user
    Sub ShowBorrowedBooks()

        Console.Write("User ID (Input with U): ")
        Dim userId As String = Console.ReadLine()


        'Check if the user exists
        Dim userFound As Boolean = False

        For i As Integer = 0 To Users.Length - 1
            Dim u() As String = Users(i).Split(","c)
            If u.Length > 0 AndAlso u(0) = userId Then
                userFound = True
                Exit For
            End If
        Next

        If Not userFound Then
            Console.WriteLine("User ID not found.")
            Return
        End If


        Console.WriteLine()
        Console.WriteLine("Borrowed books for " & userId & ":")

        Dim found As Boolean = False

        'A borrowed book entry contains a 5th field with the User ID
        For i As Integer = 0 To Libary.Length - 1

            Dim b() As String = Libary(i).Split(","c)

            If b.Length >= 5 Then
                If b(3).ToLower() = "lend" AndAlso b(4) = userId Then
                    Console.WriteLine(b(0) & " - " & b(1) & " (" & b(2) & ")")
                    found = True
                End If
            End If

        Next

        If Not found Then
            Console.WriteLine("(no borrowed books)")
        End If

    End Sub

End Module