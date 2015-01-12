Public Class passwordForm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If passwordTextBox.Text = "admin" Then
            Me.Close()
            homeForm.TabControl1.SelectTab(3)
        Else
            MsgBox("The password you have entered is incorrect.", 0, "Wrong Password")
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            passwordTextBox.UseSystemPasswordChar = False
        Else
            passwordTextBox.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub passwordTextBox_TextChanged(sender As Object, e As EventArgs) Handles passwordTextBox.TextChanged

    End Sub

   
End Class