Sub DumpSheet()
    Dim condFmtRule As FormatCondition
    Dim sht As Worksheet
    Set sht = ActiveSheet
    Dim wkbk As Workbook
    Set wkbk = ActiveWorkbook
    Dim points As Integer
    Dim chkCell As Range
    Set chkCell = sht.Cells(13, 1) ' sht.Range(sht.Cells(13, 1), sht.Cells(12, 5))
    ' is A1:V1 merged?
    If chkCell.Style <> "Heading 1" Then
        Debug.Print "2b, A1:V1, Heading 1 style not applied"
    End If
    If chkCell.Font.Size <> 20 Then
        Debug.Print "2c, A1:V1, font size(" & chkCell.Font.Size & ") not 20"
    End If
    Set chkCell = sht.Cells(3, 1)
    Dim chkCol As Integer, chkRow As Integer
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    VerifyFgBg sht, 3, 2, 3, 21, "FFFFFF", "0"
'3b) Copy this formatting to the grid coordinates in the range A4:A64 (-2 pts)
    VerifyFgBg sht, 4, 1, 64, 1, "FFFFFF", "0"
    Set chkCell = sht.Cells(4, 1)
    'Debug.Print Hex(chkCell.Font.Color)
    'Debug.Print "columns: " & chkCell.Column & "," & chkCell.Columns.Count
    'Debug.Print "row: " & chkCell.Row & "," & chkCell.Rows.Count
    Dim chkRGB As String
    ' 6a) 16 m/s ( 150, 54, 52) (-2 pts)
    ' 6b) 14 m/s (218, 150, 148)  (-2 pts)
    ' 6c) 12 m/s (230, 184, 183) (-2 pts)
    ' 6d) 10 m/s (242, 220, 219)  (-2 pts)
    ' 6e) 8 m/s (242, 242, 242) (-2 pts)
    ' 6f) 6 m/s (255, 255, 255)  (-2 pts)
    ' 6g) 4 m/s (197, 217, 241) (-2 pts)
    ' 6h) 2 m/s (141, 180, 226) (-2 pts)
    ' 6i) 0 m/s ( 83, 141, 213) (-2 pts)

    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=18", "99, 37, 35")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=16", "150, 54, 52")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=14", "218, 151, 149")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=12", "230, 185, 184")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=10", "242, 221, 220")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=8", "242, 243, 243")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=6", "255, 0, 0")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=4", "197, 218, 242")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=2", "141, 181, 227")
    chkRGB = FindRuleRGB(sht, "$B$4:$V$64", "=0", "83, 141, 214")
    'Debug.Print "Rule:" & .AppliesTo.Address & ", " & .Formula1 & ", " & GetRGB(.Interior.Color)
    '7) In the range B4:V64, reduce the font size of the values to 1 point. (-1 pt)
    'VerifyFgBg sht, 4, 2, 64, 21, 1
    '8) Enclose each of the cells in the range B4:V64 in a light gray border (-2 pts)
    
    '9) Apply the conditional highlight colors specified in Steps 5 and 6 to the legend values in the cell range X3:X12. (-2 pts)
    '10a) Merge the range Y3:Y12 (-1 pt)
    '10b) center the contents of the merged cell (-1 pt)
    '10c) rotate the text down (-2 pts)
    '10d) Display the text in a bold 18-point font.  (-1 pt)
    '11) Set the print area of the page to the range A1:Y64 (-2 pts)
    '12) On the Page Layout tab, scale the page to fit on a single page. (-2 pts)
    '13a) Add a header to the printed page with your name in the top-left header  (-2 pts)
    '13b) Add a header to the printed page with the file-name in the top-right header (-2 pts)

End Sub
Function GetRGB(c As Long) As String
    
    GetRGB = (c Mod 256)
    c = c / 256
    GetRGB = GetRGB & ", " & (c Mod 256)
    c = c / 256
    GetRGB = GetRGB & ", " & (c Mod 256)
End Function
Function CheckRequired(field As String, value As Variant) As String
    If Len("" & value) = 0 Then
        CheckRequired = "**MISSING**"
    Else
        CheckRequired = "" & value
    End If
    CheckRequired = field & ":" & CheckRequired
End Function
Function VerifyFgBg(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fg As String, bg As String) As Boolean
    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
    VerifyFgBg = True
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    For chkRow = begRow To endRow
        For chkCol = begCol To endCol
            Set chkCell = sht.Cells(chkRow, chkCol)
            With chkCell
            If Hex(.Font.Color) <> fg Then
                VerifyFgBg = False
                Debug.Print "Cell: " & .Address & ", Bad fg(" & Hex(.Font.Color) & ") s/b " & fg
            End If
            If Hex(.Interior.Color) <> bg Then
                VerifyFgBg = False
                Debug.Print "Cell: " & .Address & ", Bad bg(" & Hex(.Interior.Color) & ") s/b " & bg
            End If
            End With
        Next
    Next

End Function

Function VerifyFontSize(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fontSize As Integer) As Boolean
    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
    VerifyFontSize = True
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    For chkRow = begRow To endRow
        For chkCol = begCol To endCol
            Set chkCell = sht.Cells(chkRow, chkCol)
            With chkCell
            If .Font.Size <> fontSize Then
                VerifyFontSize = False
                Debug.Print "Cell: " & .Address & ", Bad fontSize(" & Hex(.Font.Size) & ") s/b " & fontSize
            End If
            End With
        Next
    Next
End Function

Function FindRuleRGB(sht As Worksheet, ruleRange As String, ruleExpr As String, theRGB As String) As String
'18 with fill color equal to the RGB color value ( 99, 37, 35)
    Dim condFmtRule As FormatCondition
    FindRuleRGB = ""
    For Each condFmtRule In sht.Cells.FormatConditions
        With condFmtRule
            If .AppliesTo = ruleRange And ruleExpr = .Formula1 Then
                FindRuleRGB = GetRGB(.Interior.Color)
                If FindRuleRGB <> theRGB Then
                    ' oops, rule doesn't match
                    FindRuleRGB = "Rule RGB mismatch at: " & ruleRange & " expression: " & ruleExpr & _
                        ", found rgb: " & FindRuleRGB & ", s/b: " & theRGB
                    Debug.Print FindRuleRGB
                    Exit Function
                End If
            End If
        End With
    Next
    FindRuleRGB = "Rule not found: " & ruleRange & " expression: " & ruleExpr & _
        ", rgb: " & theRGB
    Debug.Print FindRuleRGB
End Function
