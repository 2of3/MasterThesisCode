/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 0.0.1
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace CppApi {

using System;
using System.Runtime.InteropServices;

public class VectorConsumerCaller : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal VectorConsumerCaller(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(VectorConsumerCaller obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~VectorConsumerCaller() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          CppApiPINVOKE.delete_VectorConsumerCaller(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public static void CallVectorConsumer(VectorConsumer pConsumer) {
    CppApiPINVOKE.VectorConsumerCaller_CallVectorConsumer(VectorConsumer.getCPtr(pConsumer));
  }

  public VectorConsumerCaller() : this(CppApiPINVOKE.new_VectorConsumerCaller(), true) {
  }

}

}
