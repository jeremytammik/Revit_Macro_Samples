using System;
using Autodesk;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Macros;

using Revit.SDK.Samples.QuickPrint.CS;
using Revit.SDK.Samples.FindAndReplaceText.CS;
using Revit.SDK.Samples.CapitalizeText.CS;
using Revit.SDK.Samples.SearchAndReplaceWinType.CS;
using Revit.SDK.Samples.CreateBeamsColumnsBraces.CS;
using Revit.SDK.Samples.DeleteObject.CS;
using Revit.SDK.Samples.InplaceFamilyAnalyticalModel3D.CS;
using Revit.SDK.Samples.ProjectInfo.CS;
using Revit.SDK.Samples.Rooms.CS;
using Revit.SDK.Samples.RotateFramingObjects.CS;
using Revit.SDK.Samples.SlabProperties.CS;
using Revit.SDK.Samples.StructuralLayerFunction.CS;
using Revit.SDK.Samples.GridCreation.CS;

namespace MacroCSharpSamples
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInIdAttribute("C62ABA8B-D2D1-483C-B19C-10AA82F4A078")]
    public partial class ThisDocument
    {
        private void Module_Startup(object sender, EventArgs e)
        {
              
        }

        private void Module_Shutdown(object sender, EventArgs e)
        {

        }

        #region Revit Macros generated code
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Module_Startup);
            this.Shutdown += new System.EventHandler(Module_Shutdown);
        }
        #endregion

        public void CreateBeamsColumnsBraces()
        {
        	using(Transaction trans = new Transaction(this.Document, "CreateBeamsColumnsBraces"))
        	{
        		trans.Start();
        		CreateBeamsColumnsBraces sample = new CreateBeamsColumnsBraces(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void DeleteObject()
        {
        	using(Transaction trans = new Transaction(this.Document, "DeleteObject"))
        	{
        		trans.Start();
        		DeleteObject sample = new DeleteObject(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void GridCreation()
        {
        	using(Transaction trans = new Transaction(this.Document, "GridCreation"))
        	{
        		trans.Start();
        		Revit.SDK.Samples.GridCreation.CS.GridCreation sample = new Revit.SDK.Samples.GridCreation.CS.GridCreation(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void InplaceFamilyAnalyticalModel3D()
        {
            InplaceFamilyAnalyticalModel3D sample = new InplaceFamilyAnalyticalModel3D(this);
            sample.Run();
        }

        
        public void ProjectInfo()
        {
            SampleProjectInfo sample = new SampleProjectInfo(this);
            sample.Run();
        }

        public void Rooms()
        {
        	using(Transaction trans = new Transaction(this.Document, "Rooms"))
        	{
        		trans.Start();
        		SamplesRoom sample = new SamplesRoom(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void RotateFramingObjects()
        {
        	using(Transaction trans = new Transaction(this.Document, "RotateFramingObjects"))
        	{
        		trans.Start();
        		RotateFramingObjects sample = new RotateFramingObjects(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void SlabProperties()
        {
            SlabProperties sample = new SlabProperties(this);
            sample.Run();
        }

        public void StructuralLayerFunction()
        {
            StructuralLayerFunction sample = new StructuralLayerFunction(this);
            sample.Run();
        }

        public void QuickPrint_FloorPlans()
        {
            QuickPrint sample = new QuickPrint(this);
            sample.Print(ViewType.FloorPlan);
        }

        public void QuickPrint_Elevations()
        {
            QuickPrint sample = new QuickPrint(this);
            sample.Print(ViewType.Elevation);
        }

        public void QuickPrint_Section()
        {
            QuickPrint sample = new QuickPrint(this);
            sample.Print(ViewType.Section);
        }

        public void FindAndReplaceText()
        {
        	using(Transaction trans = new Transaction(this.Document, "FindAndReplaceText"))
        	{
        		trans.Start();
        		FindAndReplaceText sample = new FindAndReplaceText(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void CapitalizeText()
        {
        	using(Transaction trans = new Transaction(this.Document, "CapitalizeText"))
        	{
        		trans.Start();
        		CapitalizeText sample = new CapitalizeText(this);
        		sample.Run();
        		trans.Commit();
        	}
        }

        public void FindAndReplaceWinType()
        {
        	using(Transaction trans = new Transaction(this.Document, "FindAndReplaceWinType"))
        	{
        		trans.Start();
        		FindAndReplaceWinType sample = new FindAndReplaceWinType(this);
        		sample.Run();
        		trans.Commit();
        	}
        }
    }
}