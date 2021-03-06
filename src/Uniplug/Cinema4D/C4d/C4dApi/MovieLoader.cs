/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class MovieLoader : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal MovieLoader(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(MovieLoader obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          throw new global::System.MethodAccessException("C++ destructor does not have public access");
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public static MovieLoader Alloc() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.MovieLoader_Alloc();
    MovieLoader ret = (cPtr == global::System.IntPtr.Zero) ? null : new MovieLoader(cPtr, false);
    return ret;
  }

  public static void Free(SWIGTYPE_p_p_MovieLoader ml) {
    C4dApiPINVOKE.MovieLoader_Free(SWIGTYPE_p_p_MovieLoader.getCPtr(ml));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
  }

  public IMAGERESULT Open(Filename fn) {
    IMAGERESULT ret = (IMAGERESULT)C4dApiPINVOKE.MovieLoader_Open(swigCPtr, Filename.getCPtr(fn));
    if (C4dApiPINVOKE.SWIGPendingException.Pending) throw C4dApiPINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Close() {
    C4dApiPINVOKE.MovieLoader_Close(swigCPtr);
  }

  public BaseBitmap Read(int new_frame_idx, SWIGTYPE_p_Int32 _result) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.MovieLoader_Read__SWIG_0(swigCPtr, new_frame_idx, SWIGTYPE_p_Int32.getCPtr(_result));
    BaseBitmap ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseBitmap(cPtr, false);
    return ret;
  }

  public BaseBitmap Read(int new_frame_idx) {
    global::System.IntPtr cPtr = C4dApiPINVOKE.MovieLoader_Read__SWIG_1(swigCPtr, new_frame_idx);
    BaseBitmap ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseBitmap(cPtr, false);
    return ret;
  }

  public BaseBitmap Read() {
    global::System.IntPtr cPtr = C4dApiPINVOKE.MovieLoader_Read__SWIG_2(swigCPtr);
    BaseBitmap ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseBitmap(cPtr, false);
    return ret;
  }

  public int GetInfo(SWIGTYPE_p_Float _fps) {
    int ret = C4dApiPINVOKE.MovieLoader_GetInfo(swigCPtr, SWIGTYPE_p_Float.getCPtr(_fps));
    return ret;
  }

}

}
