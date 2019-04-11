Attribute VB_Name = "addRevenueColumn"
Sub addRevenueColumn()

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
         
      ProcessSheet (ActiveWorkbook.Worksheets(I).Name)
    
    Next I
    
    Fileout.Close

End Sub


Function ProcessSheet(ByVal sheetName As String) As String

    Sheets(sheetName).Select
    
    Dim value As String
    Dim newLine As String
    Dim outPut As String
    
    Columns("C:C").Select
    Selection.Insert Shift:=xlToRight, CopyOrigin:=xlFormatFromLeftOrAbove
    Selection.NumberFormat = "$ #,##0.00"
    Range("C1").Select
    ActiveCell.FormulaR1C1 = "entrata"
    
    
    outPut = ""
    value = ActiveSheet.Range("A2")
     
     
     
     
     Dim index As Integer
     
     index = 2
     Do While value <> vbNullString
        Range("C" & index).Select
        ActiveCell.FormulaR1C1 = "0"
        index = index + 1
        value = ActiveSheet.Range("A" & index)
     Loop
     
     ProcessSheet = outPut
    
End Function

