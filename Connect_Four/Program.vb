Imports System
Imports System.Security.Cryptography.X509Certificates

Module Program
    Sub Main()
        Dim nameOne As String 'i remember this as being a global variable when it's set up like this, but apparently not, hence why player names are passed as parameters in literally. every. sub.
        Dim nameTwo As String
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
        Console.WriteLine("Welcome to the playGame sub.")
        Call pickSymbols(playerOne, playerTwo) 'Immediately send players to pick their symbols

    End Sub
    Sub saveWin(playerOne, playerTwo)
        Console.WriteLine("Welcome to the saveWin sub.")
    End Sub
    Sub loadWin(playerOne, playerTwo)
        Console.WriteLine("Welcome to the loadWin sub.")
    End Sub
    Sub pickSymbols(nameOne, nameTwo)
        Console.WriteLine("Welcome to the pickSymbols sub.")
        Dim symbolOne As String
        Dim symbolTwo As String
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
            End If
        End While
    End Sub
End Module
