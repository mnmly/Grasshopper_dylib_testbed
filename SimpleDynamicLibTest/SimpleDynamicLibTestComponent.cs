using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace SimpleDynamicLibTest
{
    public class SimpleDynamicLibTestComponent : GH_Component
    {

        // [DllImport("libSimple.dylib")] static extern int SimpleFn(int a);

        IntPtr libraryHandle;

        [DllImport("__Internal")]
        public static extern IntPtr dlopen(string path, int flag);

        [DllImport("__Internal")]
        public static extern IntPtr dlsym(IntPtr handle, string symbolName); 

        [DllImport("__Internal")]
        public static extern int dlclose(IntPtr handle);
     
        public static IntPtr OpenLibrary(string path)
        {
            IntPtr handle = dlopen(path, 0);
            if (handle == IntPtr.Zero)
            {
                throw new Exception("Couldn't open native library: " + path);
            }
            return handle;
        }
     
        public static void CloseLibrary(IntPtr libraryHandle)
        {
            dlclose(libraryHandle);
        }
     
        public static T GetDelegate<T>(IntPtr libraryHandle, string functionName) where T : class
        {
            IntPtr symbol = dlsym(libraryHandle, functionName);
            if (symbol == IntPtr.Zero)
            {
                throw new Exception("Couldn't get function: " + functionName);
            }
            return Marshal.GetDelegateForFunctionPointer(symbol, typeof(T)) as T;
        }


        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SimpleDynamicLibTestComponent()
          : base("SimpleDynamicLibTest", "ASpi",
            "Construct an Archimedean, or arithmetic, spiral given its radii and number of turns.",
            "Curve", "Primitive")
        {
            libraryHandle = OpenLibrary("/Users/mnmly/Library/Application Support/McNeel/Rhinoceros/MacPlugIns/Grasshopper/Libraries/SimpleDynamicLibTest/libSimple.dylib");
        }

        public delegate int SimpleFn(int a);

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // Use the pManager object to register your input parameters.
            // You can often supply default values when creating parameters.
            // All parameters must have the correct access type. If you want 
            // to import lists or trees of values, modify the ParamAccess flag.
            pManager.AddTextParameter("Test", "T", "Test", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            SimpleFn fn = GetDelegate<SimpleFn>(libraryHandle, "SimpleFn");
            AddRuntimeMessage(GH_RuntimeMessageLevel.Remark, "Result from Simple Fn " + fn(10).ToString());
        }

        public override void RemovedFromDocument(GH_Document document)
        {
            CloseLibrary(libraryHandle);
            base.RemovedFromDocument(document);
        }


        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("27a32a11-ed3b-4839-9bce-ac3808180f07"); }
        }
    }
}
