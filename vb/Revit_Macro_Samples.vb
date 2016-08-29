Imports Autodesk.Revit.DB
<Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)> _
<Autodesk.Revit.DB.Macros.AddInIdAttribute("7F60C8CA-74C9-4D1F-ACBE-2A6F04004EB5")>	
Partial Class ThisDocument

    Private Sub Module_Startup(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Startup
        'hh
    End Sub

    Private Sub Module_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown

    End Sub

    Public Sub CreateBeamsColumnsBraces()
    	Dim trans = New Transaction(Me.Document, "CreateBeamsColumnsBraces")
    	trans.Start()
        Dim sample As New CreateBeamsColumnsBraces(Me)
        sample.Run()
        trans.Commit()
    End Sub 'CreateBeamsColumnsBraces

    Public Sub DeleteObject()
    	Dim trans = New Transaction(Me.Document, "DeleteObject")
    	trans.Start()
        Dim sample As New DeleteObject(Me)
        sample.Run()
        trans.Commit()
    End Sub 'DeleteObject

    Public Sub DesignOptionReader()
        Dim sample As New DesignOptionCommand(Me)
        sample.Run()
    End Sub 'DesignOptionReader

    Public Sub InplaceFamilyAnalyticalModel3D()
        Dim sample As New InplaceFamilyAnalyticalModel3D(Me)
        sample.Run()
    End Sub 'InplaceFamilyAnalyticalModel3D

    Public Sub MaterialProperties()
        Dim sample As New MaterialProperties(Me)
        sample.Run()
    End Sub 'MaterialProperties

    Public Sub RotateFramingObjects()
    	Dim trans = New Transaction(Me.Document, "RotateFramingObjects")
    	trans.Start()
        Dim sample As New RotateFramingObjects(Me)
        sample.Run()
        trans.Commit()
    End Sub 'RotateFramingObjects

    Public Sub SlabProperties()
        Dim sample As New SlabProperties(Me)
        sample.Run()
    End Sub 'SlabProperties

    Public Sub StructuralLayerFunction()
        Dim sample As New StructuralLayerFunction(Me)
        sample.Run()
    End Sub 'StructuralLayerFunction

    Public Sub TestWallThickness()
        Dim sample As New TestWallThicknessCommand(Me)
        sample.Run()
    End Sub 'TestWallThickness

    Public Sub QuickPrint_FloorPlans()
        Dim printer As New QuickPrint(Me)
        printer.Run(Autodesk.Revit.DB.ViewType.FloorPlan)
    End Sub 'QuickPrint

    Public Sub QuickPrint_Elevations()
        Dim printer As New QuickPrint(Me)
        printer.Run(Autodesk.Revit.DB.ViewType.Elevation)
    End Sub 'QuickPrint

    Public Sub QuickPrint_Sections()
        Dim printer As New QuickPrint(Me)
        printer.Run(Autodesk.Revit.DB.ViewType.Section)
    End Sub 'QuickPrint

    Public Sub FindAndReplaceText()
    	Dim trans = New Transaction(Me.Document, "FindAndReplaceText")
    	trans.Start()
        Dim sample As New FindAndReplaceText(Me)
        sample.Run()
        trans.Commit()
    End Sub 'FindAndReplaceText

    Public Sub CapitalizeText()
    	Dim trans = New Transaction(Me.Document, "CapitalizeText")
    	trans.Start()
        Dim sample As New CapitalizeText(Me)
        sample.Run()
        trans.Commit()
    End Sub 'CapitalizeText

    Public Sub FindAndReplaceWinType()
    	Dim trans = New Transaction(Me.Document, "FindAndReplaceWinType")
    	trans.Start()
        Dim sample As New FindAndReplaceWinType(Me)
        sample.Run()
        trans.Commit()
    End Sub 'FindAndReplaceWinType

End Class