Imports System.IO
Public Class homeForm
    Dim resultsChecked As Boolean

    Private Sub adminButton_Click(sender As Object, e As EventArgs) Handles adminButton.Click
        passwordForm.Show()
    End Sub

    Private Sub backButton_Click(sender As Object, e As EventArgs) Handles backButton.Click
        TabControl1.SelectTab(0)
    End Sub

    Private Sub submitButton_Click(sender As Object, e As EventArgs) Handles submitButton.Click

        'Validate form
        'end validate form

        'Get objective response data
        Dim responses As DataTable = Storage.getResponses()
        For Each response As DataRow In responses.Rows
            Dim responseGroup As FlowLayoutPanel = Me.Controls.Find("Response" + Convert.ToString(response("id")), True)(0)
            Dim rButton As RadioButton = responseGroup.Controls.OfType(Of RadioButton)().Where(Function(r) r.Checked = True).FirstOrDefault()
            If Not IsNothing(rButton) Then
                Storage.addResponse(response("id"), rButton.Text)
            End If

        Next

        'Get comment data
        Dim questions As DataTable = Storage.getQuestions()
        Dim comments() As DataRow = questions.Select("type = 'comment'")
        For Each comment In comments
            Dim commentBox As RichTextBox = Me.Controls.Find("Comment" + Convert.ToString(comment("id")), True)(0)
            'replace newlines in text with whitespaces
            Storage.addComment(comment("id"), commentBox.Text)
        Next

        If resultsCheckBox.Checked Then
            TabControl1.SelectTab(2)
        Else
            TabControl1.SelectTab(4)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectTab(0)
    End Sub

    Private Sub Results_Enter(sender As Object, e As EventArgs) Handles TabPageResults.Enter
        Dim responses As DataTable = Storage.getResponses()
        For Each response As DataRow In responses.Rows
            'create new data table for chart
            Dim chartData As DataTable = New DataTable()
            chartData.Columns.Add("option", GetType(String))
            chartData.Columns.Add("number", GetType(Integer))

            For Each c As DataColumn In responses.Columns
                If Not c.ColumnName = "id" Then
                    chartData.Rows.Add(c.ColumnName, response(c.ColumnName))
                End If


            Next

            'Assign chart data to chart
            Console.WriteLine("Chart" + Convert.ToString(response("id")))
            If Me.Controls.Find("Chart" + Convert.ToString(response("id")), True).Count > 0 Then
                Dim graph As Windows.Forms.DataVisualization.Charting.Chart = Me.Controls.Find("Chart" + Convert.ToString(response("id")), True)(0)

                If Me.Controls.Find("Chart1", True).Count > 0 Then
                    Console.WriteLine("good")
                End If
                graph.Series(0)("PieLabelStyle") = "Disabled"
                With graph.Series(0)
                    .Name = "Responses"
                    .Points.DataBind(chartData.DefaultView, "option", "number", Nothing)
                End With
            End If

        Next

    End Sub


    Private Sub homeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Default questions
        If Not File.Exists("questions.txt") Then
            Storage.addQuestion(1, "Overall, the instruction was excellent.", "objective")
            Storage.addQuestion(2, "The concepts were explained with clarity", "objective")
            Storage.addQuestion(3, "Questions and discussions were encouraged.", "objective")
            Storage.addQuestion(4, "The course was highly enjoyable.", "comment")
            Storage.addQuestion(5, "The content of the course was appropriate", "objective")
            Storage.addQuestion(6, "Text/Reference materials were appropriate for the course.", "objective")
            Storage.addQuestion(7, "Text/Reference materials were appropriate for the course.", "objective")
            Storage.addQuestion(8, "Text/Reference materials were appropriate for the course.", "comment")

        End If


        'Load question texts
        Dim questions As DataTable = Storage.getQuestions()
        For Each question As DataRow In questions.Rows
            Dim qLabels() As Control = Me.Controls.Find("qLabel" + Convert.ToString(question("id")), True)
            If qLabels.Count > 0 Then
                qLabels(0).Text = question("text")
            End If

        Next




    End Sub

    Private Sub Form_Enter(sender As Object, e As EventArgs) Handles TabPageForm.Enter
        'Load question texts
        Dim questions As DataTable = Storage.getQuestions()
        For Each question As DataRow In questions.Rows
            Dim qLabels() As Control = Me.Controls.Find("qLabel" + Convert.ToString(question("id")), True)
            If qLabels.Count > 0 Then
                qLabels(0).Text = question("text")
            End If

        Next
    End Sub

    Private Sub adminPanel_Enter(sender As Object, e As EventArgs) Handles TabPageAdmin.Enter
        Dim questions As DataTable = Storage.getQuestions()
        For Each question As DataRow In questions.Rows
            Console.WriteLine(question("text"))
            Dim qTexts() As Control = Me.Controls.Find("qText" + Convert.ToString(question("id")), True)
            If qTexts.Count > 0 Then
                qTexts(0).Text = question("text")

            End If
        Next


    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Store questions data
        Dim questions As DataTable = Storage.getQuestions()
        For Each question As DataRow In questions.Rows
            Dim qTexts() As Control = Me.Controls.Find("qText" + Convert.ToString(question("id")), True)
            If qTexts.Count > 0 Then
                Storage.editQuestion(question("id"), qTexts(0).Text)
            End If

        Next

        TabControl1.SelectTab(0)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        TabControl1.SelectTab(0)
    End Sub
    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        ProgressBar1.Value = 10
    End Sub
    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        ProgressBar1.Value = 50
    End Sub
    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        ProgressBar1.Value = 100
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        ProgressBar2.Value = 10
    End Sub
    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        ProgressBar2.Value = 50
    End Sub
    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        ProgressBar2.Value = 100
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        ProgressBar3.Value = 10
    End Sub
    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        ProgressBar3.Value = 50
    End Sub
    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        ProgressBar3.Value = 100
    End Sub


    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        ProgressBar4.Value = 10
    End Sub
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        ProgressBar4.Value = 50
    End Sub
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        ProgressBar4.Value = 100
    End Sub
    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        ProgressBar5.Value = 10
    End Sub
    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        ProgressBar5.Value = 50
    End Sub
    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        ProgressBar5.Value = 100
    End Sub
    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        ProgressBar6.Value = 10
    End Sub
    Private Sub RadioButton17_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton17.CheckedChanged
        ProgressBar6.Value = 50
    End Sub
    Private Sub RadioButton18_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton18.CheckedChanged
        ProgressBar6.Value = 100
    End Sub
    Private Sub Panel2_Click(sender As Object, e As EventArgs) Handles Panel2.Click
        Application.Restart()
    End Sub
    
    Private Sub TableLayoutPanel18_Click(sender As Object, e As EventArgs) Handles TableLayoutPanel18.Click
        Application.Restart()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        Application.Restart()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Application.Restart()
    End Sub
End Class
