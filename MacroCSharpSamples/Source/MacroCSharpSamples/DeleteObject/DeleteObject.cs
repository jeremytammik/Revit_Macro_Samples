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

using MacroCSharpSamples;

namespace Revit.SDK.Samples.DeleteObject.CS
{
    /// <summary>
    /// Delete the elements that were selected
    /// </summary>
    public class DeleteObject
    {
        ThisDocument m_doc; //ThisDocument data for Macro

        /// <summary>
        /// Ctro without parameter is not allowed
        /// </summary>
        private DeleteObject()
        {
        }

        /// <summary>
        /// Ctor with ThisDocument as 
        /// </summary>
        /// <param name="hostApp">ThisDocument handler</param>
        public DeleteObject(ThisDocument hostDoc)
        {
            m_doc = hostDoc;
        }

        /// <summary>
        /// Run this sample
        /// </summary>
        public void Run()
        {
            ElementSet collection = m_doc.Selection.Elements;
            // check user selection
            if (collection.Size < 1)
            {
                MessageBox.Show("Please select an object to delete.", "DeleteObject");
                return;
            }

            bool error = true;
            try
            {
                error = true;

                // delete selection
                IEnumerator e = collection.GetEnumerator();
                bool MoreValue = e.MoveNext();
                while (MoreValue)
                {
                    Element component = e.Current as Element;
                    m_doc.Document.Delete(component);
                    MoreValue = e.MoveNext();
                }

                error = false;
            }
            catch
            {
                // if revit threw an exception, try to catch it
                foreach (Element c in collection)
                {
                    m_doc.Selection.Elements.Insert(c);
                }
                MessageBox.Show("Element(s) can't be deleted.", "DeleteObject");
                return;
            }
            finally
            {
                // if revit threw an exception, display error and return failed
                if (error)
                {
                    MessageBox.Show("Deletion failed.");
                }
            }

            return;
        }
    }
}
