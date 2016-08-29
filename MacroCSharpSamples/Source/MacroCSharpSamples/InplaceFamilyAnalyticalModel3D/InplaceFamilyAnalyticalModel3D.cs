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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;

using Autodesk.Revit.UI.Selection;

using MacroCSharpSamples;
using System.Windows.Forms;

namespace Revit.SDK.Samples.InplaceFamilyAnalyticalModel3D.CS
{
    /// <summary>
    /// A short sample that shows how to read an analytical model 3D object 
    /// from an inplace family.
    /// </summary>
    public class InplaceFamilyAnalyticalModel3D
    {
        ThisDocument m_doc; //document data for Macro
        string m_message;

        /// <summary>
        /// Ctro without parameter is not allowed
        /// </summary>
        private InplaceFamilyAnalyticalModel3D()
        {
        }

        /// <summary>
        /// Ctor with ThisDocument as 
        /// </summary>
        /// <param name="hostApp">ThisDocument handler</param>
        public InplaceFamilyAnalyticalModel3D(ThisDocument hostDoc)
        {
            m_doc = hostDoc;
        }

        /// <summary>
        /// Run this sample
        /// </summary>
        public void Run()
        {
            try
            {
                //iterate through the selection picking out family instances that have a 3D analytical model
                ICollection<ElementId> selElements = m_doc.Selection.GetElementIds();

                if (0 == selElements.Count)
                {
                    MessageBox.Show("Please selected some in-place family instance with AnalyticalMode.",
                        "InplaceFamilyAnalyticalModel3D");
                    return;
                }

                foreach (Autodesk.Revit.DB.ElementId elementid in selElements)
                {
                	FamilyInstance familyInstance = m_doc.Document.GetElement(elementid) as FamilyInstance;

                    if (null == familyInstance)
                    {
                        MessageBox.Show("This macro depends on Revit Structure to function properly. Please open this document in Revit Structure to run this macro.", "InplaceFamilyAnaliticalModel3D");
                        continue;
                    }

                    AnalyticalModel analyticalModel = familyInstance.GetAnalyticalModel();
                    if (null == analyticalModel)
                    {
                        MessageBox.Show("This macro depends on Revit Structure to function properly. Please open this document in Revit Structure to run this macro.", "InplaceFamilyAnaliticalModel3D");
                        continue;
                    }

                    AnalyticalModel analyticalModel3D = analyticalModel as AnalyticalModel;

                    if (null == analyticalModel3D)
                    {
                        MessageBox.Show("we should select analytical model 3D family instance, but this familyInstance.AnalyticalModel type is "
                            + analyticalModel.GetType().Name, "InplaceFamilyAnaliticalModel3D");
                        continue;
                    }

                    //Output the family instance information and the curves of the analytical model. 
                    DumpFamilyInstance(familyInstance);
                    DumpAnalyticalModel3D(analyticalModel3D);
                    MessageBox.Show(m_message, "InplaceFamilyAnaliticalModel3D");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            return;
        }

        /// <summary>
        /// dump the names of the family, symbol and instance. Since the cross 
        /// section is not available it is expected that these will return something meaningful
        /// </summary>
        /// <param name="familyInstance">a fammilyInstance that have a 3D analytical model</param>
        private void DumpFamilyInstance(FamilyInstance familyInstance)
        {
          
            m_message += "Family Name : " + familyInstance.Symbol.Family.Name+ "\n"
                +"Family Symbol Name : " + familyInstance.Symbol.Name+ "\n"
                +"Family Instance Name : " + familyInstance.Name+ "\n\n";
        }

        /// <summary>
        /// dump each curve of this FamilyInstance's AnalyticalModel
        /// </summary>
        /// <param name="analyticalModel3D"></param>
        private void DumpAnalyticalModel3D(AnalyticalModel analyticalModel3D)
        {
            int counter = 1;

            // the 3D analytical model has a curves property that reports all the 
            // analytical model curves within the in place family instance
            foreach (Curve curve in analyticalModel3D.GetCurves(AnalyticalCurveType.RawCurves))
            {
                m_message += "Curve" + counter;
                
                // use the tesselate method to fragment all types of curves including lines and arcs etc.
                IList<XYZ> points = curve.Tessellate();

                foreach( XYZ point in points)
                {
                    m_message += ("\n" + point.X.ToString() + "," + point.Y.ToString() + "," + point.Z.ToString());
                }

                counter += 1;
            }
        }
    }
}
