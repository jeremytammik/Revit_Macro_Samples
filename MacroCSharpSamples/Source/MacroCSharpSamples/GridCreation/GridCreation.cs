//
// (C) Copyright 2003-2007 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using System;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

using Autodesk.Revit;
using Autodesk.Revit.DB;

using Application = Autodesk.Revit.ApplicationServices.Application;
using Element = Autodesk.Revit.DB.Element;

using SamplePropertis = MacroCSharpSamples.GridCreation.GridCreationProperties;

using MacroCSharpSamples;
using System.Diagnostics;

namespace Revit.SDK.Samples.GridCreation.CS
{
    /// <summary>
    /// Implements the Revit add-in interface IExternalCommand
    /// </summary>
    public class GridCreation
    {
#region 
        ThisDocument m_doc;
#endregion 

        /// <summary>
        /// Default constructor without parameter is not allowed 
        /// </summary>
        private GridCreation()
        {
        }

        /// <summary>
        /// GridCreation init
        /// </summary>
        /// <param name="hostApp"></param>
        public GridCreation(ThisDocument hostDoc)
        {
            m_doc = hostDoc;
        }

        /// <summary>
        /// Run this sample now
        /// </summary>
        public void Run()
        {
            try
            {
                Document document = m_doc.Document;

                // Get all selected lines and arcs 
                CurveArray selectedCurves = GetSelectedCurves(m_doc);

                // Show UI
                GridCreationOptionData gridCreationOption = new GridCreationOptionData(!selectedCurves.IsEmpty);
                using (GridCreationOptionForm gridCreationOptForm = new GridCreationOptionForm(gridCreationOption))
                {
                    DialogResult result = gridCreationOptForm.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        return ;
                    }

                    ArrayList labels = GetAllLabelsOfGrids(document);
                    DisplayUnitType dut = GetLengthUnitType(document);
                    switch (gridCreationOption.CreateGridsMode)
                    {
                        case CreateMode.Select: // Create grids with selected lines/arcs
                            CreateWithSelectedCurvesData data = new CreateWithSelectedCurvesData(m_doc, selectedCurves, labels);
                            using (CreateWithSelectedCurvesForm createWithSelected = new CreateWithSelectedCurvesForm(data))
                            {
                                result = createWithSelected.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    // Create grids
                                    data.CreateGrids();
                                }  
                            }
                            break;

                        case CreateMode.Orthogonal: // Create orthogonal grids
                            CreateOrthogonalGridsData orthogonalData = new CreateOrthogonalGridsData(m_doc, dut, labels);
                            using (CreateOrthogonalGridsForm orthogonalGridForm = new CreateOrthogonalGridsForm(orthogonalData))
                            {
                                result = orthogonalGridForm.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    // Create grids
                                    orthogonalData.CreateGrids();
                                }  
                            }
                            break;

                        case CreateMode.RadialAndArc: // Create radial and arc grids
                            CreateRadialAndArcGridsData radArcData = new CreateRadialAndArcGridsData(m_doc, dut, labels);
                            using (CreateRadialAndArcGridsForm radArcForm = new CreateRadialAndArcGridsForm(radArcData))
                            {
                                result = radArcForm.ShowDialog();
                                if (result == DialogResult.OK)
                                {
                                    // Create grids
                                    radArcData.CreateGrids();
                                }  
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Get all selected lines and arcs
        /// </summary>
        /// <param name="document">Revit's document</param>
        /// <returns>CurveArray contains all selected lines and arcs</returns>
        private  CurveArray GetSelectedCurves(ThisDocument document)
        {
            CurveArray selectedCurves = m_doc.Document.Application.Create.NewCurveArray();
            ElementSet elements = document.Selection.Elements;
            foreach (Autodesk.Revit.DB.Element element in elements)
            {
                if ((element is ModelLine) || (element is ModelArc))
                {
                    ModelCurve modelCurve = element as ModelCurve;
                    Curve curve = modelCurve.GeometryCurve;
                    if (curve != null)
                    {
                        selectedCurves.Append(curve);
                    }
                }
                else if ((element is DetailLine) || (element is DetailArc))
                {
                    DetailCurve detailCurve = element as DetailCurve;
                    Curve curve = detailCurve.GeometryCurve;
                    if (curve != null)
                    {
                        selectedCurves.Append(curve);
                    }
                }
            }

            return selectedCurves;
        }

        /// <summary>
        /// Get all model and detail lines/arcs within selected elements
        /// </summary>
        /// <param name="document">Revit's document</param>
        /// <returns>ElementSet contains all model and detail lines/arcs within selected elements </returns>
        public static ICollection<ElementId> GetSelectedModelLinesAndArcs(ThisDocument thisDocument)
        {
        	var tmpIds = new List<ElementId>();
            ElementSet elements = thisDocument.Selection.Elements;
            foreach (Autodesk.Revit.DB.Element element in elements)
            {
                if ((element is ModelLine) || (element is ModelArc) || (element is DetailLine) || (element is DetailArc))
                {
                    tmpIds.Add(element.Id);
                }
            }

            return tmpIds;
        }

        /// <summary>
        /// Get current length display unit type
        /// </summary>
        /// <param name="document">Revit's document</param>
        /// <returns>Current length display unit type</returns>
        private static DisplayUnitType GetLengthUnitType(Document document)
        {
            UnitType unittype = UnitType.UT_Length;
            ProjectUnit projectUnit = document.ProjectUnit;
            try
            {
                Autodesk.Revit.DB.FormatOptions formatOption = projectUnit.get_FormatOptions(unittype);
                return formatOption.Units;
            }
            catch (System.Exception /*e*/)
            {
                return DisplayUnitType.DUT_DECIMAL_FEET;
            }
        }

        /// <summary>
        /// Get all grid labels in current document
        /// </summary>
        /// <param name="document">Revit's document</param>
        /// <returns>ArrayList contains all grid labels in current document</returns>
        private static ArrayList GetAllLabelsOfGrids(Document document)
        {
            ArrayList labels = new ArrayList();

            ElementClassFilter gridFilter = new ElementClassFilter(typeof(Grid));
            FilteredElementCollector collector = new FilteredElementCollector(document);
            collector.WherePasses(gridFilter);
            FilteredElementIterator iter = collector.GetElementIterator();

            iter.Reset();
            while (iter.MoveNext())
            {
                Grid grid = iter.Current as Grid;
                if (null != grid)
                {
                    labels.Add(grid.Name);
                }
            }
            return labels;
        }
    }
}

