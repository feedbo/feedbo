Imports System.IO
Imports System.Type

Public Class Storage

    Private Shared _questions As DataTable
    Private Shared _responses As DataTable
    Private Shared _comments As DataTable

    Shared Sub New()
        'Initialize questions table
        _questions = New DataTable()
        _questions.Columns.Add("id", GetType(Integer))
        _questions.Columns.Add("text", GetType(String))
        _questions.Columns.Add("type", GetType(String))

        'Initialize resposnes table
        _responses = New DataTable()
        _responses.Columns.Add("id", GetType(Integer))
        _responses.Columns.Add("Disagree", GetType(Integer))
        _responses.Columns.Add("Neutral", GetType(Integer))
        _responses.Columns.Add("Agree", GetType(Integer))

        'Initialize comments table
        _comments = New DataTable()
        _comments.Columns.Add("id", GetType(Integer))
        _comments.Columns.Add("comment", GetType(String))

        'Load stored data
        If File.Exists("questions.txt") Then
            Console.WriteLine("exists")
            _questions = importTable(_questions, "questions.txt")

        End If

        If File.Exists("responses.txt") Then
            _responses = importTable(_responses, "responses.txt")

        End If


    End Sub

    Public Shared Function getQuestions() As DataTable
        Return _questions.Copy()
    End Function

    Public Shared Function getResponses() As DataTable
        Return _responses.Copy()
    End Function

    Public Shared Sub addQuestion(ByVal id As Integer, ByVal text As String, ByVal type As String)
        _questions.Rows.Add(id, text, type)
        If type = "objective" Then
            _responses.Rows.Add(id, 0, 0, 0)
            exportTable(_responses, "responses.txt")
        End If
        'export data to text file

        exportTable(_questions, "questions.txt")

    End Sub

    Public Shared Sub addResponse(ByVal id As Integer, ByVal resp As String)
        Dim rows() As DataRow = _responses.Select("id = " + Convert.ToString(id))
        Dim row As DataRow = rows(0)
        row(resp) = row(resp) + 1

        exportTable(_responses, "responses.txt")


    End Sub

    Public Shared Sub addComment(ByVal id As Integer, ByVal comment As String)
        Console.WriteLine(comment)
        _comments.Rows.Add(id, comment)
    End Sub

    Public Shared Sub editQuestion(ByVal id As Integer, ByVal text As String)
        Dim rows() As DataRow = _questions.Select("id = " + Convert.ToString(id))
        Dim row As DataRow = rows(0)
        row("text") = text
        'export table

        exportTable(_questions, "questions.txt")
    End Sub

    Public Shared Function exportTable(ByRef table As DataTable, ByVal filename As String) As Boolean
        Dim lines(table.Rows.Count) As String
        For i As Integer = 0 To table.Rows.Count - 1
            Dim rowString As String = New String("")
            Dim row As DataRow = table.Rows(i)
            For Each field As String In row.ItemArray
                rowString = rowString + field + "%"
            Next
            lines(i) = rowString
        Next
        File.WriteAllLines(filename, lines)

        Return True
    End Function

    Private Shared Function importTable(ByRef table As DataTable, ByVal filename As String) As DataTable
        Dim newTable As DataTable = table.Clone()

        Dim lines() As String = File.ReadAllLines(filename)
        Console.WriteLine(lines.Count)
        For Each line As String In lines
            If line.Length > 2 Then


                Dim rowArr() As String = line.Split(New Char() {"%"c}, StringSplitOptions.RemoveEmptyEntries)
                Console.WriteLine(newTable.Columns.Count)
                Dim row As DataRow = newTable.NewRow()
                For i As Integer = 0 To newTable.Columns.Count - 1
                    Dim c As DataColumn = newTable.Columns(i)
                    If c.DataType.ToString() = "System.Int32" Then
                        Console.WriteLine("int")
                        row(i) = Convert.ToInt32(rowArr(i))

                    Else
                        Console.WriteLine("string")

                        row(i) = rowArr(i)
                    End If

                Next

                newTable.Rows.Add(row)
            End If
        Next
        Return newTable
    End Function

    Public Shared Sub ResetForm(ByRef parent As homeForm)
        Dim form = New homeForm
        form.Show()
        parent.Close()
    End Sub
End Class
