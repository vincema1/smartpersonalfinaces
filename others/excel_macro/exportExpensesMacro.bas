Attribute VB_Name = "exportExpensesMacro"
Sub exportToCvs()

    WorksheetLoop
End Sub


 Sub WorksheetLoop()
  
    Dim fso As Object
    Dim WS_Count As Integer
    Dim I As Integer
    Dim Fileout As Object
    
    Set fso = CreateObject("Scripting.FileSystemObject")
    Set Fileout = fso.CreateTextFile(ActiveWorkbook.Path & "\output.txt", True, True)
    
    ' Set WS_Count equal to the number of worksheets in the active
    ' workbook.
    WS_Count = ActiveWorkbook.Worksheets.Count
    
    ' Begin the loop.
    For I = 1 To WS_Count
    
       ' Insert your code here.
       ' The following line shows how to reference a sheet within
       ' the loop by displaying the worksheet name in a dialog box.
      ' MsgBox ActiveWorkbook.Worksheets(I).Name
       
       Fileout.Write ProcessSheet(ActiveWorkbook.Worksheets(I).Name)
    
    Next I
    
    Fileout.Close

End Sub


Function ProcessSheet(ByVal sheetName As String) As String

     Sheets(sheetName).Select
     
     Dim value As String
     Dim newLine As String
     Dim outPut As String
     
     outPut = ""
     value = ActiveSheet.Range("A2")
     
     Dim index As Integer
     
     index = 2
     Do While value <> vbNullString
              
        newLine = ""
                    
        newLine = ActiveSheet.Range("A" & index) & "§"
        newLine = newLine & ActiveSheet.Range("B" & index) & "§"
        newLine = newLine & ActiveSheet.Range("C" & index) & "§"
        newLine = newLine & ActiveSheet.Range("D" & index) & "§"
        newLine = newLine & ActiveSheet.Range("E" & index) & "§"
        newLine = newLine & ActiveSheet.Range("F" & index) & "§"
        newLine = newLine & ActiveSheet.Range("G" & index) & vbCrLf
                    
        outPut = outPut & newLine
                    
        index = index + 1
        value = ActiveSheet.Range("A" & index)
     Loop
     
     ProcessSheet = outPut
    
End Function
