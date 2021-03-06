/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 3.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace C4d {

public class RenderNotificationData : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal RenderNotificationData(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(RenderNotificationData obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~RenderNotificationData() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          C4dApiPINVOKE.delete_RenderNotificationData(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public RenderNotificationData() : this(C4dApiPINVOKE.new_RenderNotificationData(), true) {
  }

  public BaseDocument doc {
    set {
      C4dApiPINVOKE.RenderNotificationData_doc_set(swigCPtr, BaseDocument.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.RenderNotificationData_doc_get(swigCPtr);
      BaseDocument ret = (cPtr == global::System.IntPtr.Zero) ? null : new BaseDocument(cPtr, false);
      return ret;
    } 
  }

  public bool start {
    set {
      C4dApiPINVOKE.RenderNotificationData_start_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.RenderNotificationData_start_get(swigCPtr);
      return ret;
    } 
  }

  public bool animated {
    set {
      C4dApiPINVOKE.RenderNotificationData_animated_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.RenderNotificationData_animated_get(swigCPtr);
      return ret;
    } 
  }

  public bool external {
    set {
      C4dApiPINVOKE.RenderNotificationData_external_set(swigCPtr, value);
    } 
    get {
      bool ret = C4dApiPINVOKE.RenderNotificationData_external_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_Render render {
    set {
      C4dApiPINVOKE.RenderNotificationData_render_set(swigCPtr, SWIGTYPE_p_Render.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = C4dApiPINVOKE.RenderNotificationData_render_get(swigCPtr);
      SWIGTYPE_p_Render ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_Render(cPtr, false);
      return ret;
    } 
  }

}

}
