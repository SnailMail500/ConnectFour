Imports System
Imports System.Security.Cryptography.X509Certificates

Module Program
    Dim symbolOne As String = ""
    Dim symbolTwo As String = ""
    Dim gameBoard(7, 6) As String
    Dim gameWon As Boolean = False
    Sub Main()
        Dim nameOne As String 'i remember this as being a global variable when it's set up like this, but apparently not, hence why player names are passed as parameters in literally. every. sub.
        Dim nameTwo As String REM please remember to not do stupid stuff like this unless its better (i've fallen upwards on this one)
        Dim menuOption As Integer
        Dim validInput As Boolean = False
        'Creating variables

        Console.WriteLine("Definitely NOT Connect 4 For Legal Reasons- Alex Snell")

        Console.WriteLine("Player One, please enter your name: ") 'Enter names so that the program can refer to users by their names and possibly save scores
        nameOne = Console.ReadLine()
        Console.WriteLine("Player Two, please enter your name: ")
        nameTwo = Console.ReadLine()

        Console.WriteLine("Hello, " & nameOne & " & " & nameTwo & ", welcome to the Connect 4 program!") REM program not programme, this is not on tv
        Console.WriteLine("From here, the Main Menu, you can choose from three options:")
        Console.WriteLine("1. Play Connect 4" & vbCrLf & "2. Save Previous Win (only able to be used after playing a game)" & vbCrLf & "3. Load Saved Win")
        Console.WriteLine("Please choose an option: ")

        While validInput = False
            menuOption = Console.ReadLine()
            If menuOption > 3 Or menuOption < 1 Then 'If out of range, will offer option to try again instead of throwing an error (in other words, validation)
                Console.WriteLine("Oops! That was out of range! Please try again: ")
            Else
                Console.WriteLine("Option " & menuOption & " selected.")
                validInput = True
            End If
        End While

        Select Case menuOption 'Choosing between the three options
            Case 1
                Call playGame(nameOne, nameTwo)
            Case 2
                Call saveWin(nameOne, nameTwo)
            Case 3
                Call loadWin(nameOne, nameTwo)
        End Select
    End Sub
    Sub playGame(playerOne, playerTwo)
        Dim columnNo As Integer = 0
        Dim gameWon As Boolean = False
        Dim playerNo As Integer = 1
        Dim validInput As Boolean = False
        Console.WriteLine("Welcome to the playGame sub.")
        Call pickSymbols(playerOne, playerTwo) 'Immediately send players to pick their symbols
        For i As Integer = 1 To 7
            For j As Integer = 1 To 6
                gameBoard(i, j) = " - "
            Next
        Next
        Call drawBoard()
        While gameWon = False
            If checkHorizontal() = False And checkVertical() = False And checkDiagonal() = False Then
                gameWon = False
            ElseIf checkHorizontal() = True Then
                gameWon = True
            ElseIf checkVertical() = True Then
                gameWon = True
            ElseIf checkDiagonal() = True Then
                gameWon = True
            End If
            playerNo += 1
            If playerNo Mod 2 <> 0 Then
                Console.WriteLine(playerOne & ", please choose a column number from 1-7 to place your counter: ")
                While validInput = False
                    columnNo = Console.ReadLine()
                    If columnNo > 7 Then
                        Console.WriteLine("Oops! That was too large! Please try again!")
                    ElseIf columnNo < 1 Then
                        Console.WriteLine("Oops! That was too small! Please try again!")
                    Else
                        Call placeCounter(columnNo, 1)
                        validInput = True
                    End If
                End While
                validInput = False
            ElseIf playerNo Mod 2 = 0 Then
                Console.WriteLine(playerTwo & ", please choose a column number from 1-7 to place your counter: ")
                While validInput = False
                    columnNo = Console.ReadLine()
                    If columnNo > 7 Then
                        Console.WriteLine("Oops! That was too large! Please try again!")
                    ElseIf columnNo < 1 Then
                        Console.WriteLine("Oops! That was too small! Please try again!")
                    Else
                        Call placeCounter(columnNo, 2)
                        validInput = True
                    End If
                End While
            End If
        End While
    End Sub
    Sub saveWin(playerOne, playerTwo)
        Console.WriteLine("Welcome to the saveWin sub.")
    End Sub
    Sub loadWin(playerOne, playerTwo)
        Console.WriteLine("Welcome to the loadWin sub.")
    End Sub
    Sub pickSymbols(nameOne, nameTwo)
        Console.WriteLine("Welcome to the pickSymbols sub.")
        Dim validInput As Boolean = False
        Console.WriteLine(nameOne & " you are player one. Please enter a single-character symbol below:")
        While validInput = False
            symbolOne = Console.ReadLine()
            If symbolOne.Length > 1 Then
                Console.WriteLine("Sorry! That appears to be too long. Please enter a SINGLE-CHARACTER symbol below: ")
            Else
                Console.WriteLine(nameOne & " you have selected " & symbolOne & " as your symbol.")
                validInput = True
            End If
        End While
        Console.WriteLine(nameTwo & " you are player two. Please enter a single-character symbol, different to that of Player One's, below: ")
        validInput = False
        While validInput = False
            symbolTwo = Console.ReadLine()
            If symbolTwo.Length > 1 Then
                Console.WriteLine("Sorry! That appears to be too long. Please enter a SINGLE-CHARACTER symbol, different to that of Player One's, below: ")
            ElseIf symbolTwo = symbolOne Then
                Console.WriteLine("Sorry! That's the same as Player One's symbol." & vbCrLf & "Please enter a single-character symbol, DIFFERENT TO THAT OF PLAYER ONE'S, below: ")
            Else
                Console.WriteLine(nameTwo & " you have selected " & symbolOne & " as your symbol.")
                validInput = True
            End If
        End While
    End Sub
    Sub drawBoard()
        For i As Integer = 1 To 7
            Console.WriteLine(i)
            For j As Integer = 1 To 6
                Console.WriteLine(gameBoard(j, i))
            Next
        Next
    End Sub
    Sub placeCounter(ByRef columnNo, playerNo)
        Dim currentRow As Integer = 0
        Dim validInput As Boolean = False
        Console.WriteLine("Welcome to the placeCounter sub.")
        While validInput = False
            For i As Integer = 1 To 6
                If gameBoard(i, columnNo) = " - " Then
                    currentRow = i
                    validInput = True
                Else
                    Console.WriteLine("Oops! That didn't seem to work! Please enter a free column: ")
                    Call drawBoard()
                    columnNo = Console.ReadLine()
                End If
            Next
        End While
        If playerNo = 1 Then
            gameBoard(currentRow, columnNo) = symbolOne
            Call drawBoard()
        Else
            gameBoard(currentRow, columnNo) = symbolTwo
            Call drawBoard()
        End If
    End Sub
    Function checkHorizontal()
        Dim winFound As Boolean = False
        While winFound = False
            For i As Integer = 1 To 7
                For j As Integer = 1 To 6
                    If gameBoard(j, i) = gameBoard(j, i + 1) And gameBoard(j, i) = gameBoard(j, i + 2) And gameBoard(j, i) = gameBoard(j, i + 3) And gameBoard(j, i) = gameBoard(j, i + 4) Then
                        winFound = True
                        Return True
                    ElseIf gameBoard(j, i) <> gameBoard(j, i + 1) Then
                        winFound = False
                        Return False
                    Else
                        winFound = False
                        Return False
                    End If
                Next
            Next
        End While
        Return False
    End Function
    Function checkVertical()
        Dim winFound As Boolean = False
        While winFound = False
            For i As Integer = 1 To 7
                For j As Integer = 1 To 6
                    If gameBoard(j, i) = gameBoard(j + 1, i) And gameBoard(j, i) = gameBoard(j + 2, i) And gameBoard(j, i) = gameBoard(j + 3, i) And gameBoard(j, i) = gameBoard(j + 4, i) Then
                        winFound = True
                        Return True
                    ElseIf gameBoard(j, i) <> gameBoard(j + 1, i) Then
                        winFound = False
                        Return False
                    Else
                        winFound = False
                        Return False
                    End If
                Next
            Next
        End While
        Return False
    End Function
    Function checkDiagonal()
        Return False
    End Function
End Module
