Imports System
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Security.Cryptography.X509Certificates

Module Program
    Dim symbolOne As String = ""
    Dim symbolTwo As String = ""
    Dim gameBoard(8, 7) As String
    Sub Main()
        Call makeBoard()
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
    Sub makeBoard()
        For i As Integer = 1 To 7
            For j As Integer = 1 To 6
                gameBoard(i, j) = " - "
            Next
        Next
    End Sub
    Sub playGame(playerOne, playerTwo)
        Dim columnNo As Integer = 0
        Dim gameDone As Boolean = False
        Dim playerNo As Integer = 1
        Dim validInput As Boolean = False
        Dim emptyCount As Integer = 0 'This will add up all the empty spaces in the grid so i know if no more moves can be made- I am allowing moves to be made if a draw will be forced by simply filling the grid because thats how i remember connect 4 working, most people won't realise unless they're really good, and out of slight laziness because the code will be really complex and it's quite late at night.
        Console.WriteLine("Welcome to the playGame sub.")
        Call pickSymbols(playerOne, playerTwo) 'Immediately send players to pick their symbols
        Call drawBoard()
        While gameDone = False
            If checkHorizontal() = False And checkVertical() = False And checkDiagonalRight(symbolOne) = False And checkDiagonalRight(symbolTwo) = False And checkDiagonalLeft(symbolOne) = False And checkDiagonalLeft(symbolTwo) = False Then
                REM this is horrible 
                gameDone = False
            ElseIf checkHorizontal() = True Then
                gameDone = True
            ElseIf checkVertical() = True Then
                gameDone = True
            ElseIf checkDiagonalRight(symbolOne) = True Then
                gameDone = True
            ElseIf checkDiagonalRight(symbolTwo) = True Then
                gameDone = True
            ElseIf checkDiagonalLeft(symbolOne) = True Then
                gameDone = True
            ElseIf checkDiagonalLeft(symbolTwo) = True Then
                gameDone = True
            End If
            'this whole thing is monstrous
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
            For i As Integer = 1 To 7 'After turns this checks for empty spaces on the grid so it can end the game- see comment on line 52 for the rest of the info about this
                For j As Integer = 1 To 6
                    If gameBoard(i, j) = " - " Then
                        emptyCount += 1
                    Else
                        emptyCount = emptyCount 'I realise I don't need this but it looks better in my head
                    End If
                Next
            Next
            If emptyCount = 0 Then
                gameDone = True
            ElseIf emptyCount > 0 Then
                gameDone = False 'Again unnecessary but it looks better in my head
            End If
        End While
        If gameDone = True Then
            If playerNo Mod 2 = 0 Then
                Console.WriteLine("No more moves can be made. The game was won by " & playerTwo)
            Else
                Console.WriteLine("No more moves can be made. The game was won by " & playerOne)
            End If
        End If
    End Sub
    REM THIS DOESN'T MATTER ANYMORE- ACTUAL ATTEMPTS AT SAVE AND LOAD WIN ARE AT THE BOTTOM SOMEWHERE
    'Sub saveWin(playerOne, playerTwo)
    '    Console.WriteLine("Welcome to the saveWin sub.")
    'End Sub
    'Sub loadWin(playerOne, playerTwo)
    '    Console.WriteLine("Welcome to the loadWin sub.")
    'End Sub
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
        Console.WriteLine("1  2  3  4  5  6  7 ")
        For i As Integer = 1 To 7
            For j As Integer = 1 To 6
                Console.Write(gameBoard(i, j))
            Next
            Console.Write(vbCrLf)
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
    'Split all the win checking into different functions so it's more readable and because it's easier than having one ginormous function with a million different returns
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
    Function checkDiagonalRight(ByVal symbol)
        For i As Integer = 1 To 4
            For j As Integer = 4 To 6
                If gameBoard(i, j) = symbol And gameBoard(i + 1, j - 1) = symbol And gameBoard(i + 2, j - 2) = symbol And gameBoard(i + 3, j - 3) = symbol Then
                    Return True
                Else 'not writing all that out again for an else if
                    Return False
                End If
            Next
        Next
        Return False
    End Function
    Function checkDiagonalLeft(ByVal symbol) 'Instead of using symbols here i could and maybe should have passed through a player number and got it to check, or maybe i could have added an extra if statement (but wouldn't that be repeating code which is... bad practice?)
        For i As Integer = 1 To 4
            For j As Integer = 4 To 6
                If gameBoard(i, j) = symbol And gameBoard(i, j - 1) = symbol And gameBoard(i, j - 2) = symbol And gameBoard(i, j - 3) = symbol Then
                    Return True
                Else
                    Return False
                End If
            Next
        Next
        Return False
        'If True Then
        '    Console.WriteLine("a")
        'Else
        '   Console.WriteLine("b")
        'End If
        'This was an experiment to test if an else is needed in an if statement because it wasn't obvious to me... I'm sure that I've used if without else before
    End Function
    Sub saveWin(nameOne, nameTwo)
        Console.WriteLine("You are now in the saveWin sub. ")
        'Dim openedFile As FileStream = New FileStream("saved.txt", FileMode.CreateNew, FileAccess.Write)
        'For i As Integer = 1 To 7
        '    For j As Integer = 1 To 6
        '        openedFile.Write(gameBoard(i, j))
        '    Next
        '    openedFile.Write(vbCrLf)
        'Next
        'It'll be nice in a couple of weeks when i can do file handling...
        Console.WriteLine("Coming soon, after I'm not finding out how to do file handling from some obnoxious stackoverflow post which makes no sense")
    End Sub
    Sub loadWin(nameOne, nameTwo)
        Console.WriteLine("You are now in the loadWin sub. ")
        'Dim openedFile As FileStream = New FileStream("saved.txt", FileMode.Open, FileAccess.Read)
        'Console.WriteLine(openedFile.Read)
        Console.WriteLine("Coming soon, after I'm not finding out how to do file handling from some obnoxious stackoverflow post which makes no sense")
    End Sub
End Module
'yeah, not bonkers enough to write an AI to play this against, especially given that I have definitely properly tested this before 3 on the day that it's due.