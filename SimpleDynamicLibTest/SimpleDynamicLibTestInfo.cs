using System;
using System.Drawing;
using Grasshopper;
using Grasshopper.Kernel;

namespace SimpleDynamicLibTest
{
    public class SimpleDynamicLibTestInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "SimpleDynamicLibTest Info";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("3a4acc40-c327-4de9-ac25-51b4014c9fd6");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
