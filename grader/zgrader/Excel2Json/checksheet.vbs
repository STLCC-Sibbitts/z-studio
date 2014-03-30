Sub CheckSheet()
    Dim condFmtRule As FormatCondition
    Dim sht As Worksheet
    Set sht = ActiveSheet
    Dim wkbk As Workbook
    Set wkbk = ActiveWorkbook
    Dim points As Integer
    Dim chkCell As Range
' 1) save the workbook as WizardWorks Order Form. (-1 pt)
' 3a) in cell C4, enter the customer name, Kevin Kemper (-1 pt)
    
    Debug.Print CheckRequired("2a) In the Documentation sheet, enter your name in cell B3 (-1 pt)", _
        wkbk.Worksheets("Documentation").Cells(3, 2))
    Debug.Print CheckRequired("2a) In the Documentation sheet, enter the date in cell B4  (-1 pt)", _
        wkbk.Worksheets("Documentation").Cells(4, 2))
    Dim results As String
    
    results = CheckValue("3a) in cell C4, enter the customer name:", "Kevin Kemper", "(-1 pt)", sht.Cells(4, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3b) In cell C6, enter the order number:", "28314", "(-1 pt)", sht.Cells(6, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3c) In C9 enter the following Address1:", "315 Avalon Street", "(-1 pt)", sht.Cells(9, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3d) In C10 enter Address2:", "", "(-1 pt)", sht.Cells(10, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3e) In C11 enter City:", "Greenfield", "(-1 pt)", sht.Cells(11, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3f) In C12 enter State:", "IN", "(-1 pt)", sht.Cells(12, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("3g) In C13 enter Zip:", "46140", "(-1 pt)", sht.Cells(13, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckFormula("4) In cell C5, enter a function that displays the current date.", "=TODAY()", "(-2 pts)", sht.Cells(5, 3).formula)
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5a) In B20 enter Item:", "BF001", "(-.5 pts)", sht.Cells(20, 2))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5b) In C20 enter Name:", "Bucket of Fireworks", "(-.5 pts)", sht.Cells(20, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5c) In D20 enter Price:", "45.75", "(-.5 pts)", sht.Cells(20, 4))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5d) In E20 enter Qty:", "1", "(-.5 pts)", sht.Cells(20, 5))
    If Len(results) > 0 Then Debug.Print results
    
' 5e) In B21 enter Item: NAF (-0.5 pts)
' 5f) In C21 enter Name: Nightair Fountain (-0.5 pts)
' 5g) In D21 enter Price: $ 12.95 (-0.5 pts)
' 5h) In E21 enter Qty: 4 (-0.5 pts)
    results = CheckValue("5e) In B21 enter Item:", "NAF", "(-.5 pts)", sht.Cells(21, 2))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5f) In C21 enter Name:", "Nightair Fountain", "(-.5 pts)", sht.Cells(21, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5g) In D21 enter Price:", "12.95", "(-.5 pts)", sht.Cells(21, 4))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5h) In E21 enter Qty:", "4", "(-.5 pts)", sht.Cells(21, 5))
    If Len(results) > 0 Then Debug.Print results
' 5i) In B22 enter Item:  (-0.5 pts)
' 5j) In C22 enter Name: Mountain Rockets ( Box 20) (-0.5 pts)
' 5k) In D22 enter Price: $ 55.25 (-0.5 pts)
' 5l) In E22 enter Qty: 2 , this amount gets changed to 1 in step 15. (-0.5 pts)
    results = CheckValue("5i) In B22 enter Item:", "MR20B", "(-.5 pts)", sht.Cells(22, 2))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5j) In C22 enter Name:", "Mountain Rockets (Box 20)", "(-.5 pts)", sht.Cells(22, 3))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5k) In D22 enter Price:", "55.25", "(-.5 pts)", sht.Cells(22, 4))
    If Len(results) > 0 Then Debug.Print results
    results = CheckValue("5l) In E22 enter Qty, if not 1, deduct:", "2", "(-.5 pts)", sht.Cells(22, 5))
    If sht.Cells(22, 5) <> "1" Then
        If Len(results) > 0 Then Debug.Print results
    End If


' 6) In cell C15, enter overnight to ship this order overnight. This value gets changed to standard in step 16. (-1 pt)
    results = CheckValue("6) In cell C15, enter overnight to ship this order overnight, if not standard deduct:.", "overnight", "(-1 pt)", sht.Cells(15, 3))
    If sht.Cells(15, 3) <> "standard" Then
        If Len(results) > 0 Then Debug.Print results
    End If

    results = CheckFormula("7a) In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering “”", _
        Array( _
            "=IF(E20>0,D20*E20,"""")", _
            "=IF(E20>0,D20*E20, """")", _
            "=IF(E20>0,E20*D20,"""")", _
            "=IF(E20>0,E20*D20, """")", _
            "=IF(E20>0,D20*E20,"" "")" _
            ), "(-5 pts)", sht.Cells(20, 6).formula)
    If Len(results) > 0 Then Debug.Print results
' 7b) AutoFill this formula into the range F21:F25 (-4 pts)
    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F21", _
            Array( _
            "=IF(E21>0,D21*E21,"""")", _
            "=IF(E21>0,D21*E21, """")", _
            "=IF(E21>0,E21*D21,"""")", _
            "=IF(E21>0,E21*D21, """")", _
            "=IF(E21>0,D21*E21,"" "")" _
            ), "(-1 pt)", sht.Cells(21, 6).formula)
    If Len(results) > 0 Then Debug.Print results
    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F22", _
            Array( _
            "=IF(E22>0,D22*E22,"""")", _
            "=IF(E22>0,D22*E22, """")", _
            "=IF(E22>0,E22*D22,"""")", _
            "=IF(E22>0,E22*D22, """")", _
            "=IF(E22>0,D22*E22,"" "")" _
            ), "(-1 pt)", sht.Cells(22, 6).formula)
    If Len(results) > 0 Then Debug.Print results
    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F23", _
            Array( _
            "=IF(E23>0,D23*E23, """")", _
            "=IF(E23>0,D23*E23,"""")", _
            "=IF(E23>0,E23*D23,"""")", _
            "=IF(E23>0,E23*D23, """")", _
            "=IF(E23>0,D23*E23,"" "")" _
            ), "(-1 pt)", sht.Cells(23, 6).formula)
    If Len(results) > 0 Then Debug.Print results
    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F24", _
            Array( _
            "=IF(E24>0,D24*E24,"""")", _
            "=IF(E24>0,D24*E24, """")", _
            "=IF(E24>0,E24*D24,"""")", _
            "=IF(E24>0,E24*D24, """")", _
            "=IF(E24>0,D24*E24,"" "")" _
            ), "(-1 pt)", sht.Cells(24, 6).formula)
    If Len(results) > 0 Then Debug.Print results
    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F25", _
            Array( _
            "=IF(E25>0,D25*E25,"""")", _
            "=IF(E25>0,D25*E25, """")", _
            "=IF(E25>0,E25*D25,"""")", _
            "=IF(E25>0,E25*D25, """")", _
            "=IF(E25>0,D25*E25,"" "")" _
            ), "(-1 pt)", sht.Cells(25, 6).formula)
    If Len(results) > 0 Then Debug.Print results

    results = CheckFormula("8) In cell F27, calculate the sum of the values in the range F20:F25", "=SUM(F20:F25)", _
        "(-2 pts)", sht.Cells(27, 6).formula)
    If Len(results) > 0 Then Debug.Print results

    results = CheckFormula("9) In cell F28, enter an IF function that tests whether cell F27 is greater than 200. If it is, return the value of cell F27 multiplied by the discount percentage in cell F12; otherwise, return the value 0(zero)", _
        "=IF(F27>200,F12*F27,0)", "(-5 pts)", sht.Cells(28, 6).formula)
    If Len(results) > 0 Then
        ' check alternate forms
        results = CheckFormula("9) In cell F28, enter an IF function that tests whether cell F27 is greater than 200. If it is, return the value of cell F27 multiplied by the discount percentage in cell F12; otherwise, return the value 0(zero)", _
                "=IF(F27>200,F27*F12,0)", "(-5 pts)", sht.Cells(28, 6).formula)
        If Len(results) > 0 Then Debug.Print results
    End If

    results = CheckFormula("10) In cell F29, subtract the discount value in cell F28 from the subtotal value in cell F27", _
        Array("=F27-F28", "=(F27-F28)"), "(-2 pts)", sht.Cells(29, 6).formula)
    If Len(results) > 0 Then Debug.Print results

    results = CheckFormula("11) In cell F31, calculate the sales tax by multiplying the after discount value in cell F29 by the sales tax percentage, 0.05", _
        Array("=F29*0.05", "=(F29*0.05)"), "(-2 pts)", sht.Cells(31, 6).formula)
    If Len(results) > 0 Then Debug.Print results

    results = CheckFormula("12) In cell F32, determine the shipping charge by entering an IF function that tests whether cell C15 equals “standard”. If it does, return the value in cell F9; otherwise, return the value in cell F10.", _
        "=IF(C15=""standard"",F9,F10)", "(-5 pts)", sht.Cells(32, 6).formula)
    If Len(results) > 0 Then Debug.Print results

    results = CheckFormula("13) In cell G32, display the value of cell C15 ", "=C15", "(-2 pts)", sht.Cells(32, 7).formula)
    If Len(results) > 0 Then Debug.Print results
    
    results = CheckFormula("14) In cell F34, calculate the total of the after discount value, the sales tax, and the shipping fee", _
            Array("=F29+F31+F32", "=SUM(F29,F31,F32)"), "(-2 pts)", sht.Cells(34, 6).formula)
    If Len(results) > 0 Then Debug.Print results
    
    results = CheckValue("15) Reduce the quantity of Mountain Rockets boxes from 2 to 1, and then verify that the discount is changed to 0 for the order", "1", "(-1 pt)", sht.Cells(22, 5))
    If Len(results) > 0 Then Debug.Print results

    results = CheckValue("16) Change the shipping option from overnight to standard, and then verify that the shipping fee is changed to the fee for standard shipping.", _
        "standard", " (-1 pt)", sht.Cells(15, 3))
    If Len(results) > 0 Then Debug.Print results
        
    Set sht = ActiveWorkbook.Worksheets(1)
End Sub

Function CheckValue(msg As String, reqdValue As String, pts As String, value As Variant) As String
    If Len(reqdValue) = 0 Then
        If Len("" & value) > 0 Then
            CheckValue = msg & reqdValue & ", found " & value & pts
        End If
    ElseIf Len("" & value) = 0 Then
        CheckValue = msg & reqdValue & "**MISSING**, " & pts
    ElseIf reqdValue <> value Then
        CheckValue = msg & reqdValue & ", found " & value & pts
    Else
        CheckValue = ""
    End If
End Function
Function CheckFormula(msg As String, reqdFormula As Variant, pts As String, formula As Variant) As String
    Dim chkFormula As Variant
    Dim f As Integer
    If Len("" & formula) = 0 Then
        CheckFormula = msg & " should be:" & reqdFormula & "**MISSING**, " & pts
    ElseIf IsArray(reqdFormula) Then
        'For f = 0 To UBound(reqdFormula)
        '    chkFormula = reqdFormula(f)
        'Next
        For Each chkFormula In reqdFormula
            If chkFormula = formula Then
                CheckFormula = ""
                Exit Function
            End If
            ' check for use of SUM in formula when not needed
            If Left(chkFormula, 4) <> "=SUM" Then
                If Left(formula, 4) = "=SUM" Then
                    CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & _
                        ", the SUM function should not be used when entering formulas unless cells are being SUM'ed, (-1 pt)"
                    Exit Function
                End If
            End If
        Next
        CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & pts
    ElseIf reqdFormula <> formula Then
        CheckFormula = msg & " should be:" & reqdFormula & ", found " & formula & pts
        If Left(reqdFormula, 4) <> "=SUM" Then
            If Left(formula, 4) = "=SUM" Then
                CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & _
                    ", the SUM function should not be used when entering formulas unless cells are being SUM'ed, (-1 pt)"
                Exit Function
            End If
        End If
    Else
        CheckFormula = ""
    End If
End Function

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
Function VerifyFgBg(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fg As String, bg As String, msg As String) As Boolean
    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
    VerifyFgBg = True
    Dim chkBG As Boolean
    Dim chkFG As Boolean
    chkBG = True
    chkFG = True
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    For chkRow = begRow To endRow
        For chkCol = begCol To endCol
            Set chkCell = sht.Cells(chkRow, chkCol)
            With chkCell
            If Hex(.Font.Color) <> fg Then
                If chkFG Then
                    Debug.Print msg
                    Debug.Print "Cell: " & .Address & ", Bad fg(" & Hex(.Font.Color) & ") s/b " & fg
                    chkFG = False
                End If
                VerifyFgBg = False
            End If
            If Hex(.Interior.Color) <> bg Then
                If chkBG Then
                    Debug.Print msg
                    Debug.Print "Cell: " & .Address & ", Bad bg(" & Hex(.Interior.Color) & ") s/b " & bg
                    chkBG = False
                End If
                VerifyFgBg = False
            End If
            End With
        Next
    Next

End Function

Function VerifyFontSize(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fontSize As Integer, msg As String) As Boolean
    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
    VerifyFontSize = True
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    For chkRow = begRow To endRow
        For chkCol = begCol To endCol
            Set chkCell = sht.Cells(chkRow, chkCol)
            With chkCell
            If .Font.Size <> fontSize Then
                If VerifyFontSize Then
                    Debug.Print msg
                    Debug.Print "Cell: " & .Address & ", Bad fontSize(" & .Font.Size & ") s/b " & fontSize
                    VerifyFontSize = False
                End If
            End If
            End With
        Next
    Next
End Function
Function VerifyBorderColor(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, borderColor As String, msg As String) As Boolean
    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
    VerifyBorderColor = True
    Dim ba As Boolean
    ba = True
    Dim bg As Boolean
    bg = True
'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
    For chkRow = begRow To endRow
        For chkCol = begCol To endCol
            Set chkCell = sht.Cells(chkRow, chkCol)
            With chkCell
'            Debug.Print .BorderAround
            If Not .BorderAround Then
                If ba Then
                    Debug.Print msg & "Missing border for " & .Address
                    ba = False
                End If
            End If
            ' color index = 48
            ' linestyle = 1
            ' themecolor = 1
            ' value = 1
            ' weight = 2
            ' total of 6 borders, first 4 are the outside borders
            If Hex(.Borders.Color) <> borderColor Then
                ' only check the first one
                If VerifyBorderColor = True Then
                ' see if we have a black border
                    If bc Then
                        Debug.Print msg
                        If .Borders.Color = 0 Then  ' black border
                            Debug.Print "Cell: " & .Address & ", has automatic/black border instead of: " & borderColor
                        Else
                            Debug.Print "Cell: " & .Address & ", Bad borderColor(" & Hex(.Borders.Color) & ") s/b " & borderColor
                        End If
                        bc = False
                    End If
                    VerifyBorderColor = False
                End If
            End If
            End With
        Next
    Next
End Function

Function FindRuleRGB(sht As Worksheet, ruleRange As String, ruleExpr As String, theRGB As String, Optional altRGB As String = "") As String
'18 with fill color equal to the RGB color value ( 99, 37, 35)
    Dim condFmtRule As FormatCondition
    FindRuleRGB = ""
    For Each condFmtRule In sht.Cells.FormatConditions
        With condFmtRule
            Dim loc As Integer
            'loc = InStr(1, ruleRange, "" & .AppliesTo.Address)
            loc = InStr(1, "" & .AppliesTo.Address, ruleRange)
            ' Debug.Print loc & "-" & .AppliesTo.Address & ", formula:[" & ruleExpr; "/" & .Formula1 & "]"
            ' the rule can apply to multiple ranges
            If (loc > 0) And (ruleExpr = .Formula1) Then
                FindRuleRGB = GetRGB(.Interior.Color)
                If FindRuleRGB <> theRGB Then
                    ' oops, rule doesn't match, allow for alternate value
                    If FindRuleRGB <> altRGB Then
                        FindRuleRGB = "Rule RGB mismatch at: " & ruleRange & " expression: " & ruleExpr & _
                            ", found rgb: " & FindRuleRGB & ", s/b: " & theRGB
                        Debug.Print FindRuleRGB
                        Exit Function
                    Else
                        FindRuleRGB = ""
                        Exit Function
                    End If
                Else
                    FindRuleRGB = ""
                    Exit Function
                End If
            End If
        End With
    Next
    FindRuleRGB = "Rule not found: " & ruleRange & " expression: " & ruleExpr & _
        ", rgb: " & theRGB
    Debug.Print FindRuleRGB
End Function




