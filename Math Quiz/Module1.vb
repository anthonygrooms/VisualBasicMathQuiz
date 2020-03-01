Module Module1

    Sub Main()

        'Set up data for operations, min and max boundaries
        Dim operations = {"+", "-", "*", "/"}
        Dim operation, min, max

        'Set the operation
        Console.Write("Enter +, -, * or /: ")
        operation = Console.ReadLine()

        'Assure that operation is either +,-,*,/'
        While Not operations.Contains(operation)
            Console.Write("Must enter only +, -, * or /: ")
            operation = Console.ReadLine()
        End While

        'Get the min and max values for randomization
        Console.Write("Minimum Number: ")
        min = CInt(Console.ReadLine())
        Console.Write("Maximum Number: ")
        max = CInt(Console.ReadLine())

        Console.WriteLine()

        'Declare and initialize data
        Dim numIncorrectGuesses = 0, questionCount = 0

        Dim a, b, c, answer

        'Keep asking questions as long as the user has not incorrectly guessed 3 times
        While numIncorrectGuesses < 3

            questionCount += 1 'Keep track of how many math questions the user is answering

            'Generate operands a and b
            a = RndInt(min, max)
            b = RndInt(min, max)

            'generate the correct answer c depending on the operand 
            If operation = "+" Then
                c = a + b
            ElseIf operation = "-" Then
                c = a - b
            ElseIf operation = "*" Then
                c = a * b
            Else
                'Force b to be a factor of a, but first check if b is 0 
                'to prevent a division by zero
                While b <> 0 AndAlso a Mod b <> 0
                    b = RndInt(min, max)
                    'If be is randomized to 0, exit the loop. This statement
                    'allows the possibility for b to be 0 to have the same
                    'probability as every other number that is a factor of a
                    If b = 0 Then
                        Exit While
                    End If
                End While
                If b = 0 Then
                    c = "error"
                Else
                    c = a / b
                End If
            End If

            'Ask the math question (a operation c) to the user until they get it right or run out of guesses
            While True

                Console.Write(questionCount & ") " & a & " " & operation & " " & b & " = ")

                answer = Console.ReadLine()

                If LowerCaseAllIfError(answer) = CStr(c) Then
                    Console.WriteLine(vbCrLf + "Correct! :)" + vbCrLf)
                    Exit While
                Else
                    numIncorrectGuesses += 1
                    Console.Write("Incorrect!")

                    If numIncorrectGuesses >= 3 Then
                        Console.WriteLine(vbCrLf)
                        Exit While
                    End If
                    Console.WriteLine(" Try again...")

                End If

            End While

        End While

        Console.WriteLine("You Lose!")

        Console.ReadLine()

    End Sub

    'Returns a random integer within the given bounds
    Function RndInt(lowerBound As Integer, upperBound As Integer) As Integer
        Randomize()
        RndInt = Int(lowerBound + Rnd() * (upperBound - lowerBound + 1))
    End Function

    'If the data in s spell out "error", return lowercase "error"
    'Otherwise, return s unchanged
    Function LowerCaseAllIfError(s As String) As String
        Return If(s.ToLower() = "error", "error", s)
    End Function

End Module
