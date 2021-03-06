/* Generated by JSIL v0.8.2 build 18792. See http://jsil.org/ for more information. */ 
'use strict';
var $asm04 = JSIL.DeclareAssembly("Fusee.KeyFrameAnimation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");

JSIL.DeclareNamespace("Fusee");
JSIL.DeclareNamespace("Fusee.KeyFrameAnimation");
/* class Fusee.KeyFrameAnimation.Animation */ 

(function Animation$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm09.System.Int32)) ();
  };
  var $T01 = function () {
    return ($T01 = JSIL.Memoize($asm09.System.Collections.Generic.List$b1.Of($asm04.Fusee.KeyFrameAnimation.ChannelBase))) ();
  };
  var $T02 = function () {
    return ($T02 = JSIL.Memoize($asm08.Fusee.Xirkit.Circuit)) ();
  };
  var $T03 = function () {
    return ($T03 = JSIL.Memoize($asm09.System.Type)) ();
  };
  var $T04 = function () {
    return ($T04 = JSIL.Memoize($asm09.System.Object)) ();
  };
  var $T05 = function () {
    return ($T05 = JSIL.Memoize($asm09.System.String)) ();
  };
  var $T06 = function () {
    return ($T06 = JSIL.Memoize($asm04.Fusee.KeyFrameAnimation.ChannelBase)) ();
  };
  var $T07 = function () {
    return ($T07 = JSIL.Memoize($asm09.System.Single)) ();
  };
  var $T08 = function () {
    return ($T08 = JSIL.Memoize($asm09.System.Boolean)) ();
  };
  var $T09 = function () {
    return ($T09 = JSIL.Memoize($asm08.Fusee.Xirkit.Node)) ();
  };
  var $T0A = function () {
    return ($T0A = JSIL.Memoize($asm02.Fusee.Engine.Time)) ();
  };
  var $S00 = function () {
    return ($S00 = JSIL.Memoize(new JSIL.ConstructorSignature($asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")]), null))) ();
  };
  var $S01 = function () {
    return ($S01 = JSIL.Memoize(new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")]))) ();
  };


  function Animation__ctor (animMode) {
    this._channels = $S00().Construct();
    this._animHandler = new ($T02())();
    this._time = 0;
    this._animMode = (animMode | 0);
    this._direction = true;
  }; 

  function Animation_AddAnimation$b1 (TValue, channel, channelObject, field) {
    channel.NeedTime();
    var arg_0E_0 = +this._maxDuration;
    var flag = +this._maxDuration < +channel.get_Time();
    if (flag) {
      this._maxDuration = +channel.get_Time();
    }
    $S01().CallVirtual("Add", null, this._channels, channel);
    var node = new ($T09())(channel);
    var node2 = new ($T09())(channelObject);
    node.Attach("Value", node2, field);
    (this._animHandler).AddRoot(node);
    (this._animHandler).AddNode(node2);
    (this._animHandler).Execute();
  }; 

  function Animation_AddChannel (baseChannel) {
    $S01().CallVirtual("Add", null, this._channels, baseChannel);
  }; 

  function Animation_Animate$00 (time) {
    var $temp00;
    var animMode = (this._animMode | 0);
    if (animMode !== 0) {
      if (animMode !== 1) {
        if (+this._time >= +this._maxDuration) {
          this._time = 0;
        }
      } else {
        var flag2 = ((+this._time + +time) > +this._maxDuration) || 
        ((+this._time + +time) < 0);
        if (flag2) {
          this._direction = !this._direction;
        }
      }
    } else {
      if (+this._time >= +this._maxDuration) {
        this._time = 0;
      }
    }
    var direction = this._direction;
    if (direction) {
      this._time = +this._time + +time;
    } else {
      this._time = +this._time - +time;
    }

    for (var a$0 = this._channels._items, i$0 = 0, l$0 = (this._channels._size | 0); i$0 < l$0; ($temp00 = i$0, 
        i$0 = ((i$0 + 1) | 0), 
        $temp00)) {
      var current = a$0[i$0];
      current.SetTick(this._time);
    }
    (this._animHandler).Execute();
  }; 

  function Animation_Animate$01 () {
    var $temp00;
    var num = Math.fround($T0A().get_Instance().get_DeltaTime());
    var animMode = (this._animMode | 0);
    if (animMode !== 0) {
      if (animMode !== 1) {
        if (+this._time >= +this._maxDuration) {
          this._time = 0;
        }
      } else {
        var flag2 = ((+this._time + num) > +this._maxDuration) || 
        ((+this._time + num) < 0);
        if (flag2) {
          this._direction = !this._direction;
        }
      }
    } else {
      if (+this._time >= +this._maxDuration) {
        this._time = 0;
      }
    }
    var direction = this._direction;
    if (direction) {
      this._time = +this._time + num;
    } else {
      this._time = +this._time - num;
    }

    for (var a$0 = this._channels._items, i$0 = 0, l$0 = (this._channels._size | 0); i$0 < l$0; ($temp00 = i$0, 
        i$0 = ((i$0 + 1) | 0), 
        $temp00)) {
      var current = a$0[i$0];
      current.SetTick(this._time);
    }
    (this._animHandler).Execute();
  }; 

  function Animation_DeleteAnimation (pos) {
    (this._animHandler).DeleteRoot(pos);
    (this._animHandler).DeleteNode(pos);
    (this._channels).RemoveAt(pos);
  }; 

  function Animation_get_AnimationMode () {
    return this._animMode;
  }; 

  function Animation_get_ChannelBaseList () {
    return this._channels;
  }; 

  function Animation_RemoveChannel (channelPosition) {
    (this._channels).RemoveAt(channelPosition);
  }; 

  function Animation_set_AnimationMode (value) {
    this._animMode = (value | 0);
  }; 

  function Animation_SetTick (time) {
    var $temp00;

    for (var a$0 = this._channels._items, i$0 = 0, l$0 = (this._channels._size | 0); i$0 < l$0; ($temp00 = i$0, 
        i$0 = ((i$0 + 1) | 0), 
        $temp00)) {
      var current = a$0[i$0];
      current.SetTick(time);
    }
  }; 

  JSIL.MakeType({
      BaseType: $asm09.TypeRef("System.Object"), 
      Name: "Fusee.KeyFrameAnimation.Animation", 
      IsPublic: true, 
      IsReferenceType: true, 
      MaximumConstructorArguments: 1, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      JSIL.MethodSignature.Action($.Int32), 
      Animation__ctor
    );

    $.Method({Static:false, Public:true }, "AddAnimation", 
      new JSIL.MethodSignature(null, [
          $asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1", ["!!0"]), $.Object, 
          $.String
        ], ["TValue"]), 
      Animation_AddAnimation$b1
    );

    $.Method({Static:false, Public:true }, "AddChannel", 
      JSIL.MethodSignature.Action($asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")), 
      Animation_AddChannel
    );

    $.Method({Static:false, Public:true }, "Animate", 
      JSIL.MethodSignature.Action($.Single), 
      Animation_Animate$00
    );

    $.Method({Static:false, Public:true }, "Animate", 
      JSIL.MethodSignature.Void, 
      Animation_Animate$01
    );

    $.Method({Static:false, Public:true }, "DeleteAnimation", 
      JSIL.MethodSignature.Action($.Int32), 
      Animation_DeleteAnimation
    );

    $.Method({Static:false, Public:true }, "get_AnimationMode", 
      JSIL.MethodSignature.Return($.Int32), 
      Animation_get_AnimationMode
    );

    $.Method({Static:false, Public:true }, "get_ChannelBaseList", 
      JSIL.MethodSignature.Return($asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")])), 
      Animation_get_ChannelBaseList
    );

    $.Method({Static:false, Public:true }, "RemoveChannel", 
      JSIL.MethodSignature.Action($.Int32), 
      Animation_RemoveChannel
    );

    $.Method({Static:false, Public:true }, "set_AnimationMode", 
      JSIL.MethodSignature.Action($.Int32), 
      Animation_set_AnimationMode
    );

    $.Method({Static:false, Public:true }, "SetTick", 
      JSIL.MethodSignature.Action($.Single), 
      Animation_SetTick
    );

    $.Field({Static:false, Public:false}, "_channels", $asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")]));

    $.Field({Static:false, Public:false}, "_animHandler", $asm08.TypeRef("Fusee.Xirkit.Circuit"));

    $.Field({Static:false, Public:false}, "_time", $.Single);

    $.Field({Static:false, Public:false}, "_maxDuration", $.Single);

    $.Field({Static:false, Public:false}, "_animMode", $.Int32);

    $.Field({Static:false, Public:false}, "_direction", $.Boolean);

    $.Property({Static:false, Public:true }, "ChannelBaseList", $asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase")]));

    $.Property({Static:false, Public:true }, "AnimationMode", $.Int32);


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

/* class Fusee.KeyFrameAnimation.ChannelBase */ 

(function ChannelBase$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm09.System.Single)) ();
  };


  function ChannelBase__ctor () {
  }; 

  function ChannelBase_DemandTime () {
  }; 

  function ChannelBase_DoTick (time) {
  }; 

  function ChannelBase_get_Time () {
    return this._time;
  }; 

  function ChannelBase_NeedTime () {
    this.DemandTime();
  }; 

  function ChannelBase_set_Time (value) {
    this._time = +value;
  }; 

  function ChannelBase_SetTick (time) {
    this.DoTick(time);
  }; 

  JSIL.MakeType({
      BaseType: $asm09.TypeRef("System.Object"), 
      Name: "Fusee.KeyFrameAnimation.ChannelBase", 
      IsPublic: true, 
      IsReferenceType: true, 
      MaximumConstructorArguments: 0, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      JSIL.MethodSignature.Void, 
      ChannelBase__ctor
    );

    $.Method({Static:false, Public:false, Virtual:true }, "DemandTime", 
      JSIL.MethodSignature.Void, 
      ChannelBase_DemandTime
    );

    $.Method({Static:false, Public:false, Virtual:true }, "DoTick", 
      JSIL.MethodSignature.Action($.Single), 
      ChannelBase_DoTick
    );

    $.Method({Static:false, Public:true }, "get_Time", 
      JSIL.MethodSignature.Return($.Single), 
      ChannelBase_get_Time
    );

    $.Method({Static:false, Public:true }, "NeedTime", 
      JSIL.MethodSignature.Void, 
      ChannelBase_NeedTime
    );

    $.Method({Static:false, Public:true }, "set_Time", 
      JSIL.MethodSignature.Action($.Single), 
      ChannelBase_set_Time
    );

    $.Method({Static:false, Public:true }, "SetTick", 
      JSIL.MethodSignature.Action($.Single), 
      ChannelBase_SetTick
    );

    $.Field({Static:false, Public:false}, "_time", $.Single);

    $.Property({Static:false, Public:true }, "Time", $.Single);


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

/* class Fusee.KeyFrameAnimation.Channel`1 */ 

(function Channel$b1$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm04.Fusee.KeyFrameAnimation.ChannelBase)) ();
  };
  var $T01 = function () {
    return ($T01 = JSIL.Memoize($asm09.System.Delegate)) ();
  };
  var $T02 = function () {
    return ($T02 = JSIL.Memoize($asm09.System.Threading.Interlocked)) ();
  };
  var $T03 = function () {
    return ($T03 = JSIL.Memoize($asm09.System.Boolean)) ();
  };
  var $T04 = function () {
    return ($T04 = JSIL.Memoize($asm09.System.Single)) ();
  };
  var $T05 = function () {
    return ($T05 = JSIL.Memoize($asm09.System.Int32)) ();
  };
  var $T06 = function () {
    return ($T06 = JSIL.Memoize($asm0D.System.Linq.Enumerable)) ();
  };
  var $T07 = function () {
    return ($T07 = JSIL.Memoize($asm09.System.Exception)) ();
  };
  var $T08 = function () {
    return ($T08 = JSIL.Memoize($asm09.System.ArgumentOutOfRangeException)) ();
  };
  var $T09 = function () {
    return ($T09 = JSIL.Memoize($asm02.Fusee.Engine.Diagnostics)) ();
  };
  var $S00 = function () {
    return ($S00 = JSIL.Memoize(new JSIL.MethodSignature($asm09.TypeRef("System.Boolean"), [$asm09.TypeRef("System.Collections.Generic.IEnumerable`1", ["!!0"]), $asm09.TypeRef("System.Func`2", ["!!0", $asm09.TypeRef("System.Boolean")])], ["TSource"]))) ();
  };


  function Channel$b1__ctor$00 (timeChanged, lerpFunc) {
    var $s00 = new JSIL.ConstructorSignature($asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]), null);
    this._timeline = $s00.Construct();
    this._comparer = new ($asm04.Fusee.KeyFrameAnimation.ListSort$b1.Of($thisType.TValue.get(this))) ();
    $T00().prototype._ctor.call(this);
    $thisType.Of($thisType.TValue.get(this)).prototype.add_TimeChanged.call(this, timeChanged);
    this._lerpIt = lerpFunc;
    $thisType.Of($thisType.TValue.get(this)).prototype.AddKeyframe.call(this, 0, (
        $thisType.TValue.get(this).IsValueType
           ? JSIL.CreateInstanceOfType($thisType.TValue.get(this))
           : null)
    );
  }; 

  function Channel$b1__ctor$01 (lerpFunc) {
    var $s00 = new JSIL.ConstructorSignature($asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]), null);
    this._timeline = $s00.Construct();
    this._comparer = new ($asm04.Fusee.KeyFrameAnimation.ListSort$b1.Of($thisType.TValue.get(this))) ();
    $T00().prototype._ctor.call(this);
    this._lerpIt = lerpFunc;
  }; 

  function Channel$b1__ctor$02 (lerpFunc, value) {
    var $s00 = new JSIL.ConstructorSignature($asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]), null);
    this._timeline = $s00.Construct();
    this._comparer = new ($asm04.Fusee.KeyFrameAnimation.ListSort$b1.Of($thisType.TValue.get(this))) ();
    $T00().prototype._ctor.call(this);
    this._lerpIt = lerpFunc;
    $thisType.Of($thisType.TValue.get(this)).prototype.AddKeyframe.call(this, 0, JSIL.CloneParameter($thisType.TValue.get(this), value));
  }; 

  function Channel$b1_add_TimeChanged (value) {
    var setChanelValue = this.TimeChanged;

    do {
      var setChanelValue2 = setChanelValue;
      var value2 = $T01().Combine(setChanelValue2, value);
      setChanelValue = $T02().CompareExchange$b1($asm04.Fusee.KeyFrameAnimation.Channel$b1_SetChanelValue.Of($thisType.TValue.get(this)))(/* ref */ new JSIL.MemberReference(this, "TimeChanged"), value2, setChanelValue2);
    } while (setChanelValue !== setChanelValue2);
  }; 

  function Channel$b1_AddKeyframe$03 (keyframe) {
    var $s00 = new JSIL.MethodSignature($asm09.System.Boolean, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]);
    var $s01 = new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]);
    var $s02 = new JSIL.MethodSignature(null, [$asm09.TypeRef("System.Collections.Generic.IComparer`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])])]);
    var flag = $s00.Call($thisType.Of($thisType.TValue.get(this)).prototype, "ContainsKey", null, this, keyframe);
    if (flag) {
      $thisType.Of($thisType.TValue.get(this)).prototype.RemoveKeyframe.call(this, keyframe.get_Time());
    }
    $s01.CallVirtual("Add", null, this._timeline, keyframe);
    $s02.Call($asm09.System.Collections.Generic.List$b1.Of($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this))).prototype, "Sort", null, this._timeline, this._comparer);
  }; 

  function Channel$b1_AddKeyframe$04 (time, value) {
    var $s00 = new JSIL.MethodSignature($asm09.System.Boolean, [$asm09.System.Single]);
    var $s01 = new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]);
    var $s02 = new JSIL.MethodSignature(null, [$asm09.TypeRef("System.Collections.Generic.IComparer`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])])]);
    var flag = $s00.Call($thisType.Of($thisType.TValue.get(this)).prototype, "ContainsKey", null, this, time);
    if (flag) {
      $thisType.Of($thisType.TValue.get(this)).prototype.RemoveKeyframe.call(this, time);
    }
    $s01.CallVirtual("Add", null, this._timeline, new ($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this))) (time, JSIL.CloneParameter($thisType.TValue.get(this), value)));
    $s02.Call($asm09.System.Collections.Generic.List$b1.Of($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this))).prototype, "Sort", null, this._timeline, this._comparer);
  }; 

  function Channel$b1_ContainsKey$05 (keyframe) {

    for (var i = 0; i < ((this._timeline).get_Count() | 0); ) {
      var flag = +keyframe.get_Time() === +((this._timeline).get_Item(i)).get_Time();
      if (flag) {
        var result = true;
        return result;
      }
      var num = i;
      i = ((num + 1) | 0);
    }
    result = false;
    return result;
  }; 

  function Channel$b1_ContainsKey$06 (time) {
    var $s00 = new JSIL.MethodSignature($asm09.System.Boolean, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])]);
    var $closure0 = new ($asm04.Fusee.KeyFrameAnimation.Channel$b1_$l$gc__DisplayClass19_0.Of($thisType.TValue.get(this))) ();
    $closure0.time = +time;
    return $S00().CallStatic($T06(), "Any$b1", [$asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this))], this._timeline, $asm09.System.Func$b2.Of($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)), $asm09.System.Boolean).New($closure0, function () { return $asm04.Fusee.KeyFrameAnimation.Channel$b1_$l$gc__DisplayClass19_0.Of($thisType.TValue.get(this)).prototype.$lContainsKey$gb__0.call($closure0, arguments[0]); }.bind(this), function () { return JSIL.GetMethodInfo($asm04.Fusee.KeyFrameAnimation.Channel$b1_$l$gc__DisplayClass19_0.Of($thisType.TValue.get(this)), "$lContainsKey$gb__0", $s00, false); }.bind(this)));
  }; 

  function Channel$b1_DemandTime ($exception) {
    try {
      this.set_Time(($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, ((((this._timeline).get_Count() | 0) - 1) | 0))).get_Time());
    } catch ($exception) {
      if ($T08().$Is($exception)) {
        this.set_Time(0);
      } else {
        throw $exception;
      }
    }
  }; 

  function Channel$b1_DoTick (time) {
    if (this.TimeChanged !== null) {
      var valueAt = JSIL.CloneParameter($thisType.TValue.get(this), $thisType.Of($thisType.TValue.get(this)).prototype.GetValueAt.call(this, time));
      this.TimeChanged(JSIL.CloneParameter($thisType.TValue.get(this), valueAt));
    } else {
      $thisType.Of($thisType.TValue.get(this)).prototype.GetValueAt.call(this, time);
    }
  }; 

  function Channel$b1_get_Value () {
    return this._value;
  }; 

  function Channel$b1_GetValueAt (time) {
    var flag = ((this._timeline).get_Count() | 0) > 1;
    if (flag) {
      var tValue = JSIL.CloneParameter($thisType.TValue.get(this), ($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, 0)).get_Value());

    $loop0: 
      for (var i = 1; i < ((this._timeline).get_Count() | 0); ) {
        var flag2 = (+($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, ((i - 1) | 0))).get_Time() <= +time) && 
        (+time < +($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, i)).get_Time());
        if (flag2) {
          tValue = JSIL.CloneParameter($thisType.TValue.get(this), this._lerpIt(
              JSIL.CloneParameter($thisType.TValue.get(this), ($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, ((i - 1) | 0))).get_Value()), 
              JSIL.CloneParameter($thisType.TValue.get(this), ($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, i)).get_Value()), 
              +($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, i)).get_Time() - +($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, ((i - 1) | 0))).get_Time(), 
              +time - +($T06().ElementAt$b1($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this)))(this._timeline, ((i - 1) | 0))).get_Time()
            ));
          break $loop0;
        }
        var num = i;
        i = ((num + 1) | 0);
      }
    } else {
      $T09().Log("Timeline is empty. Using default value");
      tValue = (
        $thisType.TValue.get(this).IsValueType
           ? JSIL.CreateInstanceOfType($thisType.TValue.get(this))
           : null)
      ;
    }
    this._value = JSIL.CloneParameter($thisType.TValue.get(this), tValue);
    return tValue;
  }; 

  function Channel$b1_remove_TimeChanged (value) {
    var setChanelValue = this.TimeChanged;

    do {
      var setChanelValue2 = setChanelValue;
      var value2 = $T01().Remove(setChanelValue2, value);
      setChanelValue = $T02().CompareExchange$b1($asm04.Fusee.KeyFrameAnimation.Channel$b1_SetChanelValue.Of($thisType.TValue.get(this)))(/* ref */ new JSIL.MemberReference(this, "TimeChanged"), value2, setChanelValue2);
    } while (setChanelValue !== setChanelValue2);
  }; 

  function Channel$b1_RemoveKeyframe (time) {
    var $s00 = new JSIL.MethodSignature(null, [$asm09.TypeRef("System.Collections.Generic.IComparer`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$thisType.TValue.get(this)])])]);

    for (var i = 0; i < ((this._timeline).get_Count() | 0); ) {
      var flag = +((this._timeline).get_Item(i)).get_Time() === +time;
      if (flag) {
        (this._timeline).RemoveAt(i);
      }
      $s00.Call($asm09.System.Collections.Generic.List$b1.Of($asm04.Fusee.KeyFrameAnimation.Keyframe$b1.Of($thisType.TValue.get(this))).prototype, "Sort", null, this._timeline, this._comparer);
      var num = i;
      i = ((num + 1) | 0);
    }
  }; 

  JSIL.MakeType({
      BaseType: $asm04.TypeRef("Fusee.KeyFrameAnimation.ChannelBase"), 
      Name: "Fusee.KeyFrameAnimation.Channel`1", 
      IsPublic: true, 
      IsReferenceType: true, 
      GenericParameters: ["TValue"], 
      MaximumConstructorArguments: 2, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", [$.GenericParameter("TValue")]), $asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+LerpFunc", [$.GenericParameter("TValue")])]), 
      Channel$b1__ctor$00
    );

    $.Method({Static:false, Public:true }, ".ctor", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+LerpFunc", [$.GenericParameter("TValue")])]), 
      Channel$b1__ctor$01
    );

    $.Method({Static:false, Public:true }, ".ctor", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+LerpFunc", [$.GenericParameter("TValue")]), $.GenericParameter("TValue")]), 
      Channel$b1__ctor$02
    );

    $.Method({Static:false, Public:true }, "add_TimeChanged", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", [$.GenericParameter("TValue")])]), 
      Channel$b1_add_TimeChanged
    )
      .Attribute($asm09.TypeRef("System.Runtime.CompilerServices.CompilerGeneratedAttribute"));

    $.Method({Static:false, Public:true }, "AddKeyframe", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]), 
      Channel$b1_AddKeyframe$03
    );

    $.Method({Static:false, Public:true }, "AddKeyframe", 
      new JSIL.MethodSignature(null, [$.Single, $.GenericParameter("TValue")]), 
      Channel$b1_AddKeyframe$04
    );

    $.Method({Static:false, Public:false}, "ContainsKey", 
      new JSIL.MethodSignature($.Boolean, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]), 
      Channel$b1_ContainsKey$05
    );

    $.Method({Static:false, Public:false}, "ContainsKey", 
      new JSIL.MethodSignature($.Boolean, [$.Single]), 
      Channel$b1_ContainsKey$06
    );

    $.Method({Static:false, Public:false, Virtual:true }, "DemandTime", 
      JSIL.MethodSignature.Void, 
      Channel$b1_DemandTime
    );

    $.Method({Static:false, Public:false, Virtual:true }, "DoTick", 
      JSIL.MethodSignature.Action($.Single), 
      Channel$b1_DoTick
    );

    $.Method({Static:false, Public:true }, "get_Value", 
      new JSIL.MethodSignature($.GenericParameter("TValue"), null), 
      Channel$b1_get_Value
    );

    $.Method({Static:false, Public:true }, "GetValueAt", 
      new JSIL.MethodSignature($.GenericParameter("TValue"), [$.Single]), 
      Channel$b1_GetValueAt
    );

    $.Method({Static:false, Public:true }, "remove_TimeChanged", 
      new JSIL.MethodSignature(null, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", [$.GenericParameter("TValue")])]), 
      Channel$b1_remove_TimeChanged
    )
      .Attribute($asm09.TypeRef("System.Runtime.CompilerServices.CompilerGeneratedAttribute"));

    $.Method({Static:false, Public:true }, "RemoveKeyframe", 
      JSIL.MethodSignature.Action($.Single), 
      Channel$b1_RemoveKeyframe
    );

    $.Field({Static:false, Public:false}, "TimeChanged", $asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", [$.GenericParameter("TValue")]))
      .Attribute($asm09.TypeRef("System.Runtime.CompilerServices.CompilerGeneratedAttribute"))
      .Attribute($asm09.TypeRef("System.Diagnostics.DebuggerBrowsableAttribute"), function () { return [$asm09.System.Diagnostics.DebuggerBrowsableState.Never]; });

    $.Field({Static:false, Public:false, ReadOnly:true }, "_timeline", $asm09.TypeRef("System.Collections.Generic.List`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]));

    $.Field({Static:false, Public:false, ReadOnly:true }, "_lerpIt", $asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+LerpFunc", [$.GenericParameter("TValue")]));

    $.Field({Static:false, Public:false, ReadOnly:true }, "_comparer", $asm09.TypeRef("System.Collections.Generic.IComparer`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]));

    $.Field({Static:false, Public:false}, "_value", $.GenericParameter("TValue"));

    $.Property({Static:false, Public:true }, "Value", $.GenericParameter("TValue"));

    $.Event({Static:false, Public:true }, "TimeChanged", $asm04.TypeRef("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", [$.GenericParameter("TValue")]));


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

/* delegate Fusee.KeyFrameAnimation.Channel`1+LerpFunc */ 

JSIL.MakeDelegate("Fusee.KeyFrameAnimation.Channel`1+LerpFunc", false, ["TValue"], 
  new JSIL.MethodSignature(new JSIL.GenericParameter("TValue", "Fusee.KeyFrameAnimation.Channel`1+LerpFunc"), [
      new JSIL.GenericParameter("TValue", "Fusee.KeyFrameAnimation.Channel`1+LerpFunc"), new JSIL.GenericParameter("TValue", "Fusee.KeyFrameAnimation.Channel`1+LerpFunc"), 
      $asm09.TypeRef("System.Single"), $asm09.TypeRef("System.Single")
    ]));

/* delegate Fusee.KeyFrameAnimation.Channel`1+SetChanelValue */ 

JSIL.MakeDelegate("Fusee.KeyFrameAnimation.Channel`1+SetChanelValue", false, ["TValue"], 
  new JSIL.MethodSignature(null, [new JSIL.GenericParameter("TValue", "Fusee.KeyFrameAnimation.Channel`1+SetChanelValue")]));

/* class Fusee.KeyFrameAnimation.Channel`1+<>c__DisplayClass19_0 */ 

(function $l$gc__DisplayClass19_0$Members () {
  var $, $thisType;

  function $l$gc__DisplayClass19_0__ctor () {
  }; 

  function $l$gc__DisplayClass19_0_$lContainsKey$gb__0 (t) {
    return (+this.time === +t.get_Time());
  }; 

  JSIL.MakeType({
      BaseType: $asm09.TypeRef("System.Object"), 
      Name: "Fusee.KeyFrameAnimation.Channel`1+<>c__DisplayClass19_0", 
      IsPublic: false, 
      IsReferenceType: true, 
      GenericParameters: ["TValue"], 
      MaximumConstructorArguments: 0, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      JSIL.MethodSignature.Void, 
      $l$gc__DisplayClass19_0__ctor
    );

    $.Method({Static:false, Public:false}, "$lContainsKey$gb__0", 
      new JSIL.MethodSignature($.Boolean, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]), 
      $l$gc__DisplayClass19_0_$lContainsKey$gb__0
    );

    $.Field({Static:false, Public:true }, "time", $.Single);


    return function (newThisType) { $thisType = newThisType; }; 
  })
    .Attribute($asm09.TypeRef("System.Runtime.CompilerServices.CompilerGeneratedAttribute"));

})();

/* class Fusee.KeyFrameAnimation.Keyframe`1 */ 

(function Keyframe$b1$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm09.System.Single)) ();
  };
  var $T01 = function () {
    return ($T01 = JSIL.Memoize($asm09.System.Math)) ();
  };
  var $S00 = function () {
    return ($S00 = JSIL.Memoize(new JSIL.MethodSignature($asm09.TypeRef("System.Double"), [$asm09.TypeRef("System.Double"), $asm09.TypeRef("System.Int32")]))) ();
  };


  function Keyframe$b1__ctor (time, value) {
    this._value = JSIL.CloneParameter($thisType.TValue.get(this), value);
    this._time = Math.fround($S00().CallStatic($T01(), "Round", null, time, 5));
  }; 

  function Keyframe$b1_get_Time () {
    return this._time;
  }; 

  function Keyframe$b1_get_Value () {
    return this._value;
  }; 

  function Keyframe$b1_set_Time (value) {
    this._time = Math.fround($S00().CallStatic($T01(), "Round", null, value, 5));
  }; 

  function Keyframe$b1_set_Value (value) {
    this._value = JSIL.CloneParameter($thisType.TValue.get(this), value);
  }; 

  JSIL.MakeType({
      BaseType: $asm09.TypeRef("System.Object"), 
      Name: "Fusee.KeyFrameAnimation.Keyframe`1", 
      IsPublic: true, 
      IsReferenceType: true, 
      GenericParameters: ["TValue"], 
      MaximumConstructorArguments: 2, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      new JSIL.MethodSignature(null, [$.Single, $.GenericParameter("TValue")]), 
      Keyframe$b1__ctor
    );

    $.Method({Static:false, Public:true }, "get_Time", 
      JSIL.MethodSignature.Return($.Single), 
      Keyframe$b1_get_Time
    );

    $.Method({Static:false, Public:true }, "get_Value", 
      new JSIL.MethodSignature($.GenericParameter("TValue"), null), 
      Keyframe$b1_get_Value
    );

    $.Method({Static:false, Public:true }, "set_Time", 
      JSIL.MethodSignature.Action($.Single), 
      Keyframe$b1_set_Time
    );

    $.Method({Static:false, Public:true }, "set_Value", 
      new JSIL.MethodSignature(null, [$.GenericParameter("TValue")]), 
      Keyframe$b1_set_Value
    );

    $.Field({Static:false, Public:false}, "_value", $.GenericParameter("TValue"));

    $.Field({Static:false, Public:false}, "_time", $.Single);

    $.Property({Static:false, Public:true }, "Time", $.Single);

    $.Property({Static:false, Public:true }, "Value", $.GenericParameter("TValue"));


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

/* class Fusee.KeyFrameAnimation.Lerp */ 

(function Lerp$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm09.System.Double)) ();
  };
  var $T01 = function () {
    return ($T01 = JSIL.Memoize($asm09.System.Single)) ();
  };
  var $T02 = function () {
    return ($T02 = JSIL.Memoize($asm05.Fusee.Math.float2)) ();
  };
  var $T03 = function () {
    return ($T03 = JSIL.Memoize($asm05.Fusee.Math.float3)) ();
  };
  var $T04 = function () {
    return ($T04 = JSIL.Memoize($asm05.Fusee.Math.Quaternion)) ();
  };
  var $T05 = function () {
    return ($T05 = JSIL.Memoize($asm05.Fusee.Math.float4)) ();
  };
  var $T06 = function () {
    return ($T06 = JSIL.Memoize($asm09.System.Int32)) ();
  };


  function Lerp_DoubleLerp (val1, val2, time1, time2) {
    return (+val1 + (+(((+val2 - +val1) / time1)) * time2));
  }; 

  function Lerp_Float2Lerp (val1, val2, time1, time2) {
    var result = new ($T02())();
    result.x = +val1.x + (+(((+val2.x - +val1.x) / +time1)) * +time2);
    result.y = +val1.y + (+(((+val2.y - +val1.y) / +time1)) * +time2);
    return result;
  }; 

  function Lerp_Float3Lerp (val1, val2, time1, time2) {
    var result = new ($T03())();
    result.x = +val1.x + (+(((+val2.x - +val1.x) / +time1)) * +time2);
    result.y = +val1.y + (+(((+val2.y - +val1.y) / +time1)) * +time2);
    result.z = +val1.z + (+(((+val2.z - +val1.z) / +time1)) * +time2);
    return result;
  }; 

  function Lerp_Float3QuaternionSlerp (val1, val2, time1, time2) {
    var q = $T04().EulerToQuaternion(val1.MemberwiseClone(), false);
    var q2 = $T04().EulerToQuaternion(val2.MemberwiseClone(), false);
    var q3 = $T04().Slerp(q.MemberwiseClone(), q2.MemberwiseClone(), +((+time2 / +time1))).MemberwiseClone();
    return $T04().QuaternionToEuler(q3.MemberwiseClone(), false);
  }; 

  function Lerp_Float4Lerp (val1, val2, time1, time2) {
    var result = new ($T05())();
    result.w = +val1.w + (+(((+val2.w - +val1.w) / +time1)) * +time2);
    result.x = +val1.x + (+(((+val2.x - +val1.x) / +time1)) * +time2);
    result.y = +val1.y + (+(((+val2.y - +val1.y) / +time1)) * +time2);
    result.z = +val1.z + (+(((+val2.z - +val1.z) / +time1)) * +time2);
    return result;
  }; 

  function Lerp_FloatLerp (val1, val2, time1, time2) {
    return (+val1 + (+(((+val2 - +val1) / +time1)) * +time2));
  }; 

  function Lerp_IntLerp (val1, val2, time1, time2) {
    return ((+val1 + (+((+((val2 | 0) - (val1 | 0)) / +time1)) * +time2)) | 0);
  }; 

  JSIL.MakeStaticClass("Fusee.KeyFrameAnimation.Lerp", true, [], function ($ib) {
    $ = $ib;

    $.Method({Static:true , Public:true }, "DoubleLerp", 
      new JSIL.MethodSignature($.Double, [
          $.Double, $.Double, 
          $.Single, $.Single
        ]), 
      Lerp_DoubleLerp
    );

    $.Method({Static:true , Public:true }, "Float2Lerp", 
      new JSIL.MethodSignature($asm05.TypeRef("Fusee.Math.float2"), [
          $asm05.TypeRef("Fusee.Math.float2"), $asm05.TypeRef("Fusee.Math.float2"), 
          $.Single, $.Single
        ]), 
      Lerp_Float2Lerp
    );

    $.Method({Static:true , Public:true }, "Float3Lerp", 
      new JSIL.MethodSignature($asm05.TypeRef("Fusee.Math.float3"), [
          $asm05.TypeRef("Fusee.Math.float3"), $asm05.TypeRef("Fusee.Math.float3"), 
          $.Single, $.Single
        ]), 
      Lerp_Float3Lerp
    );

    $.Method({Static:true , Public:true }, "Float3QuaternionSlerp", 
      new JSIL.MethodSignature($asm05.TypeRef("Fusee.Math.float3"), [
          $asm05.TypeRef("Fusee.Math.float3"), $asm05.TypeRef("Fusee.Math.float3"), 
          $.Single, $.Single
        ]), 
      Lerp_Float3QuaternionSlerp
    );

    $.Method({Static:true , Public:true }, "Float4Lerp", 
      new JSIL.MethodSignature($asm05.TypeRef("Fusee.Math.float4"), [
          $asm05.TypeRef("Fusee.Math.float4"), $asm05.TypeRef("Fusee.Math.float4"), 
          $.Single, $.Single
        ]), 
      Lerp_Float4Lerp
    );

    $.Method({Static:true , Public:true }, "FloatLerp", 
      new JSIL.MethodSignature($.Single, [
          $.Single, $.Single, 
          $.Single, $.Single
        ]), 
      Lerp_FloatLerp
    );

    $.Method({Static:true , Public:true }, "IntLerp", 
      new JSIL.MethodSignature($.Int32, [
          $.Int32, $.Int32, 
          $.Single, $.Single
        ]), 
      Lerp_IntLerp
    );


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

/* class Fusee.KeyFrameAnimation.ListSort`1 */ 

(function ListSort$b1$Members () {
  var $, $thisType;
  var $T00 = function () {
    return ($T00 = JSIL.Memoize($asm09.System.Int32)) ();
  };
  var $T01 = function () {
    return ($T01 = JSIL.Memoize($asm09.System.Single)) ();
  };
  var $T02 = function () {
    return ($T02 = JSIL.Memoize($asm09.System.Boolean)) ();
  };


  function ListSort$b1__ctor () {
  }; 

  function ListSort$b1_Compare (x, y) {
    var num = (JSIL.CompareValues(x.get_Time(), y.get_Time()));
    var flag = num === 0;
    if (flag) {
      var result = (JSIL.CompareValues(x.get_Time(), y.get_Time()));
    } else {
      result = num;
    }
    return result;
  }; 

  JSIL.MakeType({
      BaseType: $asm09.TypeRef("System.Object"), 
      Name: "Fusee.KeyFrameAnimation.ListSort`1", 
      IsPublic: true, 
      IsReferenceType: true, 
      GenericParameters: ["TValue"], 
      MaximumConstructorArguments: 0, 
    }, function ($ib) {
    $ = $ib;

    $.Method({Static:false, Public:true }, ".ctor", 
      JSIL.MethodSignature.Void, 
      ListSort$b1__ctor
    );

    $.Method({Static:false, Public:true , Virtual:true }, "Compare", 
      new JSIL.MethodSignature($.Int32, [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")]), $asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])]), 
      ListSort$b1_Compare
    );

    $.ImplementInterfaces(
      /* 0 */ $asm09.TypeRef("System.Collections.Generic.IComparer`1", [$asm04.TypeRef("Fusee.KeyFrameAnimation.Keyframe`1", [$.GenericParameter("TValue")])])
    );


    return function (newThisType) { $thisType = newThisType; }; 
  });

})();

