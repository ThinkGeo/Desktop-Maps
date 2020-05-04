# MapView


## Inheritance Hierarchy

+ `Object`
  + `DispatcherObject`
    + `DependencyObject`
      + `Visual`
        + `UIElement`
          + `FrameworkElement`
            + `Control`
              + **`MapView`**

## Members Summary

### Public Constructors Summary


|Name|
|---|
|[`MapView()`](#mapview)|

### Protected Constructors Summary


|Name|
|---|
|N/A|

### Public Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`ActualHeight`](#actualheight)|`Double`|N/A|
|[`ActualWidth`](#actualwidth)|`Double`|N/A|
|[`AdornmentOverlay`](#adornmentoverlay)|[`AdornmentOverlay`](ThinkGeo.UI.Wpf.AdornmentOverlay.md)|Gets or sets the adornment overlay in the MapControl.|
|[`AllowDrop`](#allowdrop)|`Boolean`|N/A|
|[`AreAnyTouchesCaptured`](#areanytouchescaptured)|`Boolean`|N/A|
|[`AreAnyTouchesCapturedWithin`](#areanytouchescapturedwithin)|`Boolean`|N/A|
|[`AreAnyTouchesDirectlyOver`](#areanytouchesdirectlyover)|`Boolean`|N/A|
|[`AreAnyTouchesOver`](#areanytouchesover)|`Boolean`|N/A|
|[`Background`](#background)|`Brush`|N/A|
|[`BackgroundOverlay`](#backgroundoverlay)|[`BackgroundOverlay`](ThinkGeo.UI.Wpf.BackgroundOverlay.md)|Gets or sets the background overlay.|
|[`BindingGroup`](#bindinggroup)|`BindingGroup`|N/A|
|[`BitmapEffect`](#bitmapeffect)|`BitmapEffect`|N/A|
|[`BitmapEffectInput`](#bitmapeffectinput)|`BitmapEffectInput`|N/A|
|[`BorderBrush`](#borderbrush)|`Brush`|N/A|
|[`BorderThickness`](#borderthickness)|`Thickness`|N/A|
|[`CacheMode`](#cachemode)|`CacheMode`|N/A|
|[`Clip`](#clip)|`Geometry`|N/A|
|[`ClipToBounds`](#cliptobounds)|`Boolean`|N/A|
|[`CommandBindings`](#commandbindings)|`CommandBindingCollection`|N/A|
|[`ContextMenu`](#contextmenu)|`ContextMenu`|N/A|
|[`CurrentExtent`](#currentextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|Gets or sets the current extent of the MapControl.|
|[`CurrentScale`](#currentscale)|`Double`|Gets or sets current viewport scale.|
|[`Cursor`](#cursor)|`Cursor`|N/A|
|[`DataContext`](#datacontext)|`Object`|N/A|
|[`DependencyObjectType`](#dependencyobjecttype)|`DependencyObjectType`|N/A|
|[`DesiredSize`](#desiredsize)|`Size`|N/A|
|[`Dispatcher`](#dispatcher)|`Dispatcher`|N/A|
|[`EditOverlay`](#editoverlay)|[`EditInteractiveOverlay`](ThinkGeo.UI.Wpf.EditInteractiveOverlay.md)|Gets or sets the edit overlay in the MapControl.|
|[`Effect`](#effect)|`Effect`|N/A|
|[`ExtentOverlay`](#extentoverlay)|[`ExtentInteractiveOverlay`](ThinkGeo.UI.Wpf.ExtentInteractiveOverlay.md)|Gets or sets the extent overlay in the MapControl.|
|[`FlowDirection`](#flowdirection)|`FlowDirection`|N/A|
|[`Focusable`](#focusable)|`Boolean`|N/A|
|[`FocusVisualStyle`](#focusvisualstyle)|`Style`|N/A|
|[`FontFamily`](#fontfamily)|`FontFamily`|N/A|
|[`FontSize`](#fontsize)|`Double`|N/A|
|[`FontStretch`](#fontstretch)|`FontStretch`|N/A|
|[`FontStyle`](#fontstyle)|`FontStyle`|N/A|
|[`FontWeight`](#fontweight)|`FontWeight`|N/A|
|[`ForceCursor`](#forcecursor)|`Boolean`|N/A|
|[`Foreground`](#foreground)|`Brush`|N/A|
|[`HasAnimatedProperties`](#hasanimatedproperties)|`Boolean`|N/A|
|[`Height`](#height)|`Double`|N/A|
|[`HorizontalAlignment`](#horizontalalignment)|`HorizontalAlignment`|N/A|
|[`HorizontalContentAlignment`](#horizontalcontentalignment)|`HorizontalAlignment`|N/A|
|[`InputBindings`](#inputbindings)|`InputBindingCollection`|N/A|
|[`InputScope`](#inputscope)|`InputScope`|N/A|
|[`InteractiveOverlays`](#interactiveoverlays)|GeoCollection<[`InteractiveOverlay`](ThinkGeo.UI.Wpf.InteractiveOverlay.md)>|This property gets the collection of InteractiveOverlays in the MapControl.|
|[`IsArrangeValid`](#isarrangevalid)|`Boolean`|N/A|
|[`IsEnabled`](#isenabled)|`Boolean`|N/A|
|[`IsFocused`](#isfocused)|`Boolean`|N/A|
|[`IsHitTestVisible`](#ishittestvisible)|`Boolean`|N/A|
|[`IsInitialized`](#isinitialized)|`Boolean`|N/A|
|[`IsInputMethodEnabled`](#isinputmethodenabled)|`Boolean`|N/A|
|[`IsKeyboardFocused`](#iskeyboardfocused)|`Boolean`|N/A|
|[`IsKeyboardFocusWithin`](#iskeyboardfocuswithin)|`Boolean`|N/A|
|[`IsLoaded`](#isloaded)|`Boolean`|N/A|
|[`IsManipulationEnabled`](#ismanipulationenabled)|`Boolean`|N/A|
|[`IsMeasureValid`](#ismeasurevalid)|`Boolean`|N/A|
|[`IsMouseCaptured`](#ismousecaptured)|`Boolean`|N/A|
|[`IsMouseCaptureWithin`](#ismousecapturewithin)|`Boolean`|N/A|
|[`IsMouseDirectlyOver`](#ismousedirectlyover)|`Boolean`|N/A|
|[`IsMouseOver`](#ismouseover)|`Boolean`|N/A|
|[`IsSealed`](#issealed)|`Boolean`|N/A|
|[`IsStylusCaptured`](#isstyluscaptured)|`Boolean`|N/A|
|[`IsStylusCaptureWithin`](#isstyluscapturewithin)|`Boolean`|N/A|
|[`IsStylusDirectlyOver`](#isstylusdirectlyover)|`Boolean`|N/A|
|[`IsStylusOver`](#isstylusover)|`Boolean`|N/A|
|[`IsTabStop`](#istabstop)|`Boolean`|N/A|
|[`IsVisible`](#isvisible)|`Boolean`|N/A|
|[`Language`](#language)|`XmlLanguage`|N/A|
|[`LayoutTransform`](#layouttransform)|`Transform`|N/A|
|[`MapResizeMode`](#mapresizemode)|[`MapResizeMode`](ThinkGeo.UI.Wpf.MapResizeMode.md)|Gets a strategy for changing extent when resizes map control.|
|[`MapTools`](#maptools)|[`MapTools`](ThinkGeo.UI.Wpf.MapTools.md)|Gets a object for simply using MapTools.|
|[`MapUnit`](#mapunit)|[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)|Gets or sets the map unit used by the MapControl.|
|[`Margin`](#margin)|`Thickness`|N/A|
|[`MaxHeight`](#maxheight)|`Double`|N/A|
|[`MaximumScale`](#maximumscale)|`Double`|This property gets and sets a maximum scale when navigating the map. When you keep zooming out, and the target scale is bigger than the maximum scale, the zooming operation will be stopped.|
|[`MaxWidth`](#maxwidth)|`Double`|N/A|
|[`MinHeight`](#minheight)|`Double`|N/A|
|[`MinimumScale`](#minimumscale)|`Double`|This property gets and sets a minimum scale when navigating the map. When you keep zooming in, and the target scale is smaller than the minimum scale, the zooming operation will be stopped.|
|[`MinWidth`](#minwidth)|`Double`|N/A|
|[`Name`](#name)|`String`|N/A|
|[`Opacity`](#opacity)|`Double`|N/A|
|[`OpacityMask`](#opacitymask)|`Brush`|N/A|
|[`Overlays`](#overlays)|GeoCollection<[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)>|This property gets the collection of Overlays in the MapControl.|
|[`OverridesDefaultStyle`](#overridesdefaultstyle)|`Boolean`|N/A|
|[`Padding`](#padding)|`Thickness`|N/A|
|[`Parent`](#parent)|`DependencyObject`|N/A|
|[`PersistId`](#persistid)|`Int32`|N/A|
|[`PivotScreenPoint`](#pivotscreenpoint)|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|N/A|
|[`RenderSize`](#rendersize)|`Size`|N/A|
|[`RenderTransform`](#rendertransform)|`Transform`|N/A|
|[`RenderTransformOrigin`](#rendertransformorigin)|`Point`|N/A|
|[`Resources`](#resources)|`ResourceDictionary`|N/A|
|[`RestrictExtent`](#restrictextent)|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|This property gets or sets an extent to restrict the map navigation.|
|[`RotatedAngle`](#rotatedangle)|`Single`|N/A|
|[`SnapsToDevicePixels`](#snapstodevicepixels)|`Boolean`|N/A|
|[`Style`](#style)|`Style`|N/A|
|[`TabIndex`](#tabindex)|`Int32`|N/A|
|[`Tag`](#tag)|`Object`|N/A|
|[`Template`](#template)|`ControlTemplate`|N/A|
|[`TemplatedParent`](#templatedparent)|`DependencyObject`|N/A|
|[`ToolTip`](#tooltip)|`Object`|N/A|
|[`TouchesCaptured`](#touchescaptured)|IEnumerable<`TouchDevice`>|N/A|
|[`TouchesCapturedWithin`](#touchescapturedwithin)|IEnumerable<`TouchDevice`>|N/A|
|[`TouchesDirectlyOver`](#touchesdirectlyover)|IEnumerable<`TouchDevice`>|N/A|
|[`TouchesOver`](#touchesover)|IEnumerable<`TouchDevice`>|N/A|
|[`TrackOverlay`](#trackoverlay)|[`TrackInteractiveOverlay`](ThinkGeo.UI.Wpf.TrackInteractiveOverlay.md)|Gets or sets the track overlay in the MapControl.|
|[`Triggers`](#triggers)|`TriggerCollection`|N/A|
|[`Uid`](#uid)|`String`|N/A|
|[`UseLayoutRounding`](#uselayoutrounding)|`Boolean`|N/A|
|[`VerticalAlignment`](#verticalalignment)|`VerticalAlignment`|N/A|
|[`VerticalContentAlignment`](#verticalcontentalignment)|`VerticalAlignment`|N/A|
|[`Visibility`](#visibility)|`Visibility`|N/A|
|[`Width`](#width)|`Double`|N/A|
|[`ZoomLevelSet`](#zoomlevelset)|[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)|This property gets or sets the ZoomLevelSet used for the WpfMap control.|

### Protected Properties Summary

|Name|Return Type|Description|
|---|---|---|
|[`AncestorChangeInProgress`](#ancestorchangeinprogress)|`Boolean`|N/A|
|[`Animatable_IsResourceInvalidationNecessary`](#animatable_isresourceinvalidationnecessary)|`Boolean`|N/A|
|[`AreTransformsClean`](#aretransformsclean)|`Boolean`|N/A|
|[`ArrangeDirty`](#arrangedirty)|`Boolean`|N/A|
|[`ArrangeInProgress`](#arrangeinprogress)|`Boolean`|N/A|
|[`BitmapEffectEmulationDisabled`](#bitmapeffectemulationdisabled)|`Boolean`|N/A|
|[`BypassLayoutPolicies`](#bypasslayoutpolicies)|`Boolean`|N/A|
|[`CacheModeChangedHandler`](#cachemodechangedhandler)|`EventHandler`|N/A|
|[`CanBeInheritanceContext`](#canbeinheritancecontext)|`Boolean`|N/A|
|[`ClipChangedHandler`](#clipchangedhandler)|`EventHandler`|N/A|
|[`ClipToBoundsCache`](#cliptoboundscache)|`Boolean`|N/A|
|[`CommandBindingsInternal`](#commandbindingsinternal)|`CommandBindingCollection`|N/A|
|[`ContentsChangedHandler`](#contentschangedhandler)|`EventHandler`|N/A|
|[`DefaultStyleKey`](#defaultstylekey)|`Object`|N/A|
|[`DTypeThemeStyleKey`](#dtypethemestylekey)|`DependencyObjectType`|N/A|
|[`EffectChangedHandler`](#effectchangedhandler)|`EventHandler`|N/A|
|[`EffectiveValues`](#effectivevalues)|`EffectiveValueEntry[]`|N/A|
|[`EffectiveValuesCount`](#effectivevaluescount)|`UInt32`|N/A|
|[`EffectiveValuesInitialSize`](#effectivevaluesinitialsize)|`Int32`|N/A|
|[`EventCanvas`](#eventcanvas)|`Canvas`|Gets a canvas control to hook all the events of the map.|
|[`EventHandlersStore`](#eventhandlersstore)|`EventHandlersStore`|N/A|
|[`Freezable_Frozen`](#freezable_frozen)|`Boolean`|N/A|
|[`Freezable_HasMultipleInheritanceContexts`](#freezable_hasmultipleinheritancecontexts)|`Boolean`|N/A|
|[`Freezable_UsingContextList`](#freezable_usingcontextlist)|`Boolean`|N/A|
|[`Freezable_UsingHandlerList`](#freezable_usinghandlerlist)|`Boolean`|N/A|
|[`Freezable_UsingSingletonContext`](#freezable_usingsingletoncontext)|`Boolean`|N/A|
|[`Freezable_UsingSingletonHandler`](#freezable_usingsingletonhandler)|`Boolean`|N/A|
|[`GuidelinesChangedHandler`](#guidelineschangedhandler)|`EventHandler`|N/A|
|[`HandlesScrolling`](#handlesscrolling)|`Boolean`|N/A|
|[`HasAutomationPeer`](#hasautomationpeer)|`Boolean`|N/A|
|[`HasEffectiveKeyboardFocus`](#haseffectivekeyboardfocus)|`Boolean`|N/A|
|[`HasFefLoadedChangeHandler`](#hasfefloadedchangehandler)|`Boolean`|N/A|
|[`HasImplicitStyleFromResources`](#hasimplicitstylefromresources)|`Boolean`|N/A|
|[`HasLocalStyle`](#haslocalstyle)|`Boolean`|N/A|
|[`HasLogicalChildren`](#haslogicalchildren)|`Boolean`|N/A|
|[`HasMultipleInheritanceContexts`](#hasmultipleinheritancecontexts)|`Boolean`|N/A|
|[`HasNumberSubstitutionChanged`](#hasnumbersubstitutionchanged)|`Boolean`|N/A|
|[`HasResourceReference`](#hasresourcereference)|`Boolean`|N/A|
|[`HasResources`](#hasresources)|`Boolean`|N/A|
|[`HasStyleChanged`](#hasstylechanged)|`Boolean`|N/A|
|[`HasStyleEverBeenFetched`](#hasstyleeverbeenfetched)|`Boolean`|N/A|
|[`HasStyleInvalidated`](#hasstyleinvalidated)|`Boolean`|N/A|
|[`HasTemplateChanged`](#hastemplatechanged)|`Boolean`|N/A|
|[`HasTemplateGeneratedSubTree`](#hastemplategeneratedsubtree)|`Boolean`|N/A|
|[`HasThemeStyleEverBeenFetched`](#hasthemestyleeverbeenfetched)|`Boolean`|N/A|
|[`HasVisualChildren`](#hasvisualchildren)|`Boolean`|N/A|
|[`IAnimatable_HasAnimatedProperties`](#ianimatable_hasanimatedproperties)|`Boolean`|N/A|
|[`InheritableEffectiveValuesCount`](#inheritableeffectivevaluescount)|`UInt32`|N/A|
|[`InheritableProperties`](#inheritableproperties)|FrugalObjectList<`DependencyProperty`>|N/A|
|[`InheritanceBehavior`](#inheritancebehavior)|`InheritanceBehavior`|N/A|
|[`InheritanceContext`](#inheritancecontext)|`DependencyObject`|N/A|
|[`InheritanceParent`](#inheritanceparent)|`DependencyObject`|N/A|
|[`InputBindingsInternal`](#inputbindingsinternal)|`InputBindingCollection`|N/A|
|[`InternalVisual2DOr3DChildrenCount`](#internalvisual2dor3dchildrencount)|`Int32`|N/A|
|[`InternalVisualChildrenCount`](#internalvisualchildrencount)|`Int32`|N/A|
|[`InternalVisualParent`](#internalvisualparent)|`DependencyObject`|N/A|
|[`InVisibilityCollapsedTree`](#invisibilitycollapsedtree)|`Boolean`|N/A|
|[`IsEnabledCore`](#isenabledcore)|`Boolean`|N/A|
|[`IsInheritanceContextSealed`](#isinheritancecontextsealed)|`Boolean`|N/A|
|[`IsLoadedCache`](#isloadedcache)|`Boolean`|N/A|
|[`IsLogicalChildrenIterationInProgress`](#islogicalchildreniterationinprogress)|`Boolean`|N/A|
|[`IsParentAnFE`](#isparentanfe)|`Boolean`|N/A|
|[`IsRequestingExpression`](#isrequestingexpression)|`Boolean`|N/A|
|[`IsRightToLeft`](#isrighttoleft)|`Boolean`|N/A|
|[`IsRootElement`](#isrootelement)|`Boolean`|N/A|
|[`IsSelfInheritanceParent`](#isselfinheritanceparent)|`Boolean`|N/A|
|[`IsStyleSetFromGenerator`](#isstylesetfromgenerator)|`Boolean`|N/A|
|[`IsStyleUpdateInProgress`](#isstyleupdateinprogress)|`Boolean`|N/A|
|[`IsTemplatedParentAnFE`](#istemplatedparentanfe)|`Boolean`|N/A|
|[`IsTemplateRoot`](#istemplateroot)|`Boolean`|N/A|
|[`IsThemeStyleUpdateInProgress`](#isthemestyleupdateinprogress)|`Boolean`|N/A|
|[`IsVisualChildrenIterationInProgress`](#isvisualchildreniterationinprogress)|`Boolean`|N/A|
|[`LoadedPending`](#loadedpending)|`Object[]`|N/A|
|[`LogicalChildren`](#logicalchildren)|`IEnumerator`|N/A|
|[`MeasureDirty`](#measuredirty)|`Boolean`|N/A|
|[`MeasureDuringArrange`](#measureduringarrange)|`Boolean`|N/A|
|[`MeasureInProgress`](#measureinprogress)|`Boolean`|N/A|
|[`NeverArranged`](#neverarranged)|`Boolean`|N/A|
|[`NeverMeasured`](#nevermeasured)|`Boolean`|N/A|
|[`OpacityMaskChangedHandler`](#opacitymaskchangedhandler)|`EventHandler`|N/A|
|[`OverlayCanvas`](#overlaycanvas)|`Canvas`|Gets a canvas control to maintain current overlays in the DOM tree.|
|[`PositionAndSizeOfSetController`](#positionandsizeofsetcontroller)|`UIElement`|N/A|
|[`PotentiallyHasMentees`](#potentiallyhasmentees)|`Boolean`|N/A|
|[`PreviousArrangeRect`](#previousarrangerect)|`Rect`|N/A|
|[`PreviousConstraint`](#previousconstraint)|`Size`|N/A|
|[`ScrollableAreaClipChangedHandler`](#scrollableareaclipchangedhandler)|`EventHandler`|N/A|
|[`ShouldLookupImplicitStyles`](#shouldlookupimplicitstyles)|`Boolean`|N/A|
|[`SnapsToDevicePixelsCache`](#snapstodevicepixelscache)|`Boolean`|N/A|
|[`StateGroupsRoot`](#stategroupsroot)|`FrameworkElement`|N/A|
|[`StoresParentTemplateValues`](#storesparenttemplatevalues)|`Boolean`|N/A|
|[`StylusPlugIns`](#stylusplugins)|`StylusPlugInCollection`|N/A|
|[`SubtreeHasLoadedChangeHandler`](#subtreehasloadedchangehandler)|`Boolean`|N/A|
|[`TemplateCache`](#templatecache)|`FrameworkTemplate`|N/A|
|[`TemplateChild`](#templatechild)|`UIElement`|N/A|
|[`TemplateChildIndex`](#templatechildindex)|`Int32`|N/A|
|[`TemplateInternal`](#templateinternal)|`FrameworkTemplate`|N/A|
|[`ThemeStyle`](#themestyle)|`Style`|N/A|
|[`ThisHasLoadedChangeEventHandler`](#thishasloadedchangeeventhandler)|`Boolean`|N/A|
|[`ToolsGrid`](#toolsgrid)|`Grid`|Gets a grid control to hold all the map tools.|
|[`TransformChangedHandler`](#transformchangedhandler)|`EventHandler`|N/A|
|[`TreeLevel`](#treelevel)|`UInt32`|N/A|
|[`UnloadedPending`](#unloadedpending)|`Object[]`|N/A|
|[`VisualBitmapEffect`](#visualbitmapeffect)|`BitmapEffect`|N/A|
|[`VisualBitmapEffectInput`](#visualbitmapeffectinput)|`BitmapEffectInput`|N/A|
|[`VisualBitmapEffectInputInternal`](#visualbitmapeffectinputinternal)|`BitmapEffectInput`|N/A|
|[`VisualBitmapEffectInternal`](#visualbitmapeffectinternal)|`BitmapEffect`|N/A|
|[`VisualBitmapScalingMode`](#visualbitmapscalingmode)|`BitmapScalingMode`|N/A|
|[`VisualCacheMode`](#visualcachemode)|`CacheMode`|N/A|
|[`VisualChildrenCount`](#visualchildrencount)|`Int32`|N/A|
|[`VisualClearTypeHint`](#visualcleartypehint)|`ClearTypeHint`|N/A|
|[`VisualClip`](#visualclip)|`Geometry`|N/A|
|[`VisualContentBounds`](#visualcontentbounds)|`Rect`|N/A|
|[`VisualDescendantBounds`](#visualdescendantbounds)|`Rect`|N/A|
|[`VisualEdgeMode`](#visualedgemode)|`EdgeMode`|N/A|
|[`VisualEffect`](#visualeffect)|`Effect`|N/A|
|[`VisualEffectInternal`](#visualeffectinternal)|`Effect`|N/A|
|[`VisualOffset`](#visualoffset)|`Vector`|N/A|
|[`VisualOpacity`](#visualopacity)|`Double`|N/A|
|[`VisualOpacityMask`](#visualopacitymask)|`Brush`|N/A|
|[`VisualParent`](#visualparent)|`DependencyObject`|N/A|
|[`VisualScrollableAreaClip`](#visualscrollableareaclip)|Nullable<`Rect`>|N/A|
|[`VisualStateChangeSuspended`](#visualstatechangesuspended)|`Boolean`|N/A|
|[`VisualTextHintingMode`](#visualtexthintingmode)|`TextHintingMode`|N/A|
|[`VisualTextRenderingMode`](#visualtextrenderingmode)|`TextRenderingMode`|N/A|
|[`VisualTransform`](#visualtransform)|`Transform`|N/A|
|[`VisualXSnappingGuidelines`](#visualxsnappingguidelines)|`DoubleCollection`|N/A|
|[`VisualYSnappingGuidelines`](#visualysnappingguidelines)|`DoubleCollection`|N/A|

### Public Methods Summary


|Name|
|---|
|[`AddHandler(RoutedEvent,Delegate)`](#addhandlerroutedeventdelegate)|
|[`AddHandler(RoutedEvent,Delegate,Boolean)`](#addhandlerroutedeventdelegateboolean)|
|[`AddToEventRoute(EventRoute,RoutedEventArgs)`](#addtoeventrouteeventrouteroutedeventargs)|
|[`ApplyAnimationClock(DependencyProperty,AnimationClock)`](#applyanimationclockdependencypropertyanimationclock)|
|[`ApplyAnimationClock(DependencyProperty,AnimationClock,HandoffBehavior)`](#applyanimationclockdependencypropertyanimationclockhandoffbehavior)|
|[`ApplyTemplate()`](#applytemplate)|
|[`Arrange(Rect)`](#arrangerect)|
|[`BeginAnimation(DependencyProperty,AnimationTimeline)`](#beginanimationdependencypropertyanimationtimeline)|
|[`BeginAnimation(DependencyProperty,AnimationTimeline,HandoffBehavior)`](#beginanimationdependencypropertyanimationtimelinehandoffbehavior)|
|[`BeginInit()`](#begininit)|
|[`BeginStoryboard(Storyboard)`](#beginstoryboardstoryboard)|
|[`BeginStoryboard(Storyboard,HandoffBehavior)`](#beginstoryboardstoryboardhandoffbehavior)|
|[`BeginStoryboard(Storyboard,HandoffBehavior,Boolean)`](#beginstoryboardstoryboardhandoffbehaviorboolean)|
|[`BringIntoView()`](#bringintoview)|
|[`BringIntoView(Rect)`](#bringintoviewrect)|
|[`CaptureMouse()`](#capturemouse)|
|[`CaptureStylus()`](#capturestylus)|
|[`CaptureTouch(TouchDevice)`](#capturetouchtouchdevice)|
|[`CenterAt(PointShape)`](#centeratpointshape)|
|[`CenterAt(ScreenPointF)`](#centeratscreenpointf)|
|[`CenterAt(Feature)`](#centeratfeature)|
|[`CenterAt(Single,Single)`](#centeratsinglesingle)|
|[`CheckAccess()`](#checkaccess)|
|[`ClearValue(DependencyProperty)`](#clearvaluedependencyproperty)|
|[`ClearValue(DependencyPropertyKey)`](#clearvaluedependencypropertykey)|
|[`CoerceValue(DependencyProperty)`](#coercevaluedependencyproperty)|
|[`Dispose()`](#dispose)|
|[`EndInit()`](#endinit)|
|[`Equals(Object)`](#equalsobject)|
|[`FindCommonVisualAncestor(DependencyObject)`](#findcommonvisualancestordependencyobject)|
|[`FindFeatureLayer(String)`](#findfeaturelayerstring)|
|[`FindName(String)`](#findnamestring)|
|[`FindRasterLayer(String)`](#findrasterlayerstring)|
|[`FindResource(Object)`](#findresourceobject)|
|[`Focus()`](#focus)|
|[`GetAnimationBaseValue(DependencyProperty)`](#getanimationbasevaluedependencyproperty)|
|[`GetBindingExpression(DependencyProperty)`](#getbindingexpressiondependencyproperty)|
|[`GetHashCode()`](#gethashcode)|
|[`GetLocalValueEnumerator()`](#getlocalvalueenumerator)|
|[`GetSnappedExtent(RectangleShape)`](#getsnappedextentrectangleshape)|
|[`GetSnappedZoomLevelIndex(RectangleShape)`](#getsnappedzoomlevelindexrectangleshape)|
|[`GetSnappedZoomLevelIndex(Double)`](#getsnappedzoomlevelindexdouble)|
|[`GetType()`](#gettype)|
|[`GetValue(DependencyProperty)`](#getvaluedependencyproperty)|
|[`GetVersion()`](#getversion)|
|[`InputHitTest(Point)`](#inputhittestpoint)|
|[`InvalidateArrange()`](#invalidatearrange)|
|[`InvalidateMeasure()`](#invalidatemeasure)|
|[`InvalidateProperty(DependencyProperty)`](#invalidatepropertydependencyproperty)|
|[`InvalidateVisual()`](#invalidatevisual)|
|[`IsAncestorOf(DependencyObject)`](#isancestorofdependencyobject)|
|[`IsDescendantOf(DependencyObject)`](#isdescendantofdependencyobject)|
|[`LoadState(Byte[])`](#loadstatebyte[])|
|[`Measure(Size)`](#measuresize)|
|[`MoveFocus(TraversalRequest)`](#movefocustraversalrequest)|
|[`OnApplyTemplate()`](#onapplytemplate)|
|[`Pan(Single,Int32)`](#pansingleint32)|
|[`Pan(PanDirection,Int32)`](#panpandirectionint32)|
|[`Pan(Double,Double)`](#pandoubledouble)|
|[`PointFromScreen(Point)`](#pointfromscreenpoint)|
|[`PointToScreen(Point)`](#pointtoscreenpoint)|
|[`PredictFocus(FocusNavigationDirection)`](#predictfocusfocusnavigationdirection)|
|[`RaiseEvent(RoutedEventArgs)`](#raiseeventroutedeventargs)|
|[`ReadLocalValue(DependencyProperty)`](#readlocalvaluedependencyproperty)|
|[`Refresh(OverlayRefreshType)`](#refreshoverlayrefreshtype)|
|[`Refresh(RectangleShape,OverlayRefreshType)`](#refreshrectangleshapeoverlayrefreshtype)|
|[`Refresh(Overlay,OverlayRefreshType)`](#refreshoverlayoverlayrefreshtype)|
|[`Refresh(IEnumerable<Overlay>,OverlayRefreshType)`](#refreshienumerable<overlay>overlayrefreshtype)|
|[`Refresh(RectangleShape,Overlay,OverlayRefreshType)`](#refreshrectangleshapeoverlayoverlayrefreshtype)|
|[`Refresh(RectangleShape,IEnumerable<Overlay>,OverlayRefreshType)`](#refreshrectangleshapeienumerable<overlay>overlayrefreshtype)|
|[`RegisterName(String,Object)`](#registernamestringobject)|
|[`ReleaseAllTouchCaptures()`](#releasealltouchcaptures)|
|[`ReleaseMouseCapture()`](#releasemousecapture)|
|[`ReleaseStylusCapture()`](#releasestyluscapture)|
|[`ReleaseTouchCapture(TouchDevice)`](#releasetouchcapturetouchdevice)|
|[`RemoveHandler(RoutedEvent,Delegate)`](#removehandlerroutedeventdelegate)|
|[`SaveState()`](#savestate)|
|[`SetBinding(DependencyProperty,BindingBase)`](#setbindingdependencypropertybindingbase)|
|[`SetBinding(DependencyProperty,String)`](#setbindingdependencypropertystring)|
|[`SetCurrentValue(DependencyProperty,Object)`](#setcurrentvaluedependencypropertyobject)|
|[`SetResourceReference(DependencyProperty,Object)`](#setresourcereferencedependencypropertyobject)|
|[`SetValue(DependencyProperty,Object)`](#setvaluedependencypropertyobject)|
|[`SetValue(DependencyPropertyKey,Object)`](#setvaluedependencypropertykeyobject)|
|[`ShouldSerializeCommandBindings()`](#shouldserializecommandbindings)|
|[`ShouldSerializeInputBindings()`](#shouldserializeinputbindings)|
|[`ShouldSerializeResources()`](#shouldserializeresources)|
|[`ShouldSerializeStyle()`](#shouldserializestyle)|
|[`ShouldSerializeTriggers()`](#shouldserializetriggers)|
|[`ToggleMapExtents()`](#togglemapextents)|
|[`ToScreenCoordinate(Double,Double)`](#toscreencoordinatedoubledouble)|
|[`ToScreenCoordinate(PointShape)`](#toscreencoordinatepointshape)|
|[`ToScreenCoordinate(Point)`](#toscreencoordinatepoint)|
|[`ToString()`](#tostring)|
|[`ToWorldCoordinate(Double,Double)`](#toworldcoordinatedoubledouble)|
|[`ToWorldCoordinate(PointShape)`](#toworldcoordinatepointshape)|
|[`ToWorldCoordinate(Point)`](#toworldcoordinatepoint)|
|[`TransformToAncestor(Visual)`](#transformtoancestorvisual)|
|[`TransformToAncestor(Visual3D)`](#transformtoancestorvisual3d)|
|[`TransformToDescendant(Visual)`](#transformtodescendantvisual)|
|[`TransformToVisual(Visual)`](#transformtovisualvisual)|
|[`TranslatePoint(Point,UIElement)`](#translatepointpointuielement)|
|[`TryFindResource(Object)`](#tryfindresourceobject)|
|[`UnregisterName(String)`](#unregisternamestring)|
|[`UpdateDefaultStyle()`](#updatedefaultstyle)|
|[`UpdateLayout()`](#updatelayout)|
|[`VerifyAccess()`](#verifyaccess)|
|[`ZoomIn()`](#zoomin)|
|[`ZoomIn(Int32)`](#zoominint32)|
|[`ZoomIntoCenter(Int32,Feature)`](#zoomintocenterint32feature)|
|[`ZoomIntoCenter(Int32,PointShape)`](#zoomintocenterint32pointshape)|
|[`ZoomIntoCenter(Int32,ScreenPointF)`](#zoomintocenterint32screenpointf)|
|[`ZoomIntoCenter(Int32,Single,Single)`](#zoomintocenterint32singlesingle)|
|[`ZoomOut()`](#zoomout)|
|[`ZoomOut(Int32)`](#zoomoutint32)|
|[`ZoomOutToCenter(Int32,Feature)`](#zoomouttocenterint32feature)|
|[`ZoomOutToCenter(Int32,PointShape)`](#zoomouttocenterint32pointshape)|
|[`ZoomOutToCenter(Int32,ScreenPointF)`](#zoomouttocenterint32screenpointf)|
|[`ZoomOutToCenter(Int32,Single,Single)`](#zoomouttocenterint32singlesingle)|
|[`ZoomTo(PointShape,Double)`](#zoomtopointshapedouble)|
|[`ZoomToPreviousExtent()`](#zoomtopreviousextent)|
|[`ZoomToScale(Double)`](#zoomtoscaledouble)|
|[`ZoomToScale(Double,ScreenPointF)`](#zoomtoscaledoublescreenpointf)|
|[`ZoomToScale(Double,Single,Single)`](#zoomtoscaledoublesinglesingle)|

### Protected Methods Summary


|Name|
|---|
|[`AddInheritanceContext(DependencyObject,DependencyProperty)`](#addinheritancecontextdependencyobjectdependencyproperty)|
|[`AddLogicalChild(Object)`](#addlogicalchildobject)|
|[`AddRefOnChannelCore(Channel)`](#addrefonchannelcorechannel)|
|[`AddRefOnChannelForCyclicBrush(ICyclicBrush,Channel)`](#addrefonchannelforcyclicbrushicyclicbrushchannel)|
|[`AddSynchronizedInputPostOpportunityHandler(EventRoute,RoutedEventArgs)`](#addsynchronizedinputpostopportunityhandlereventrouteroutedeventargs)|
|[`AddSynchronizedInputPreOpportunityHandler(EventRoute,RoutedEventArgs)`](#addsynchronizedinputpreopportunityhandlereventrouteroutedeventargs)|
|[`AddSynchronizedInputPreOpportunityHandlerCore(EventRoute,RoutedEventArgs)`](#addsynchronizedinputpreopportunityhandlercoreeventrouteroutedeventargs)|
|[`AddToEventRouteCore(EventRoute,RoutedEventArgs)`](#addtoeventroutecoreeventrouteroutedeventargs)|
|[`AddVisualChild(Visual)`](#addvisualchildvisual)|
|[`AdjustBranchSource(RoutedEventArgs)`](#adjustbranchsourceroutedeventargs)|
|[`AdjustEventSource(RoutedEventArgs)`](#adjusteventsourceroutedeventargs)|
|[`ArrangeCore(Rect)`](#arrangecorerect)|
|[`ArrangeOverride(Size)`](#arrangeoverridesize)|
|[`BeginPropertyInitialization()`](#beginpropertyinitialization)|
|[`BitmapEffectEmulationChanged(Object,EventArgs)`](#bitmapeffectemulationchangedobjecteventargs)|
|[`BlockReverseInheritance()`](#blockreverseinheritance)|
|[`BuildRoute(EventRoute,RoutedEventArgs)`](#buildrouteeventrouteroutedeventargs)|
|[`BuildRouteCore(EventRoute,RoutedEventArgs)`](#buildroutecoreeventrouteroutedeventargs)|
|[`BuildRouteCoreHelper(EventRoute,RoutedEventArgs,Boolean)`](#buildroutecorehelpereventrouteroutedeventargsboolean)|
|[`CacheModeChanged(Object,EventArgs)`](#cachemodechangedobjecteventargs)|
|[`CalculateSubgraphBoundsInnerSpace()`](#calculatesubgraphboundsinnerspace)|
|[`CalculateSubgraphBoundsInnerSpace(Boolean)`](#calculatesubgraphboundsinnerspaceboolean)|
|[`CalculateSubgraphBoundsOuterSpace()`](#calculatesubgraphboundsouterspace)|
|[`CalculateSubgraphRenderBoundsInnerSpace()`](#calculatesubgraphrenderboundsinnerspace)|
|[`CalculateSubgraphRenderBoundsOuterSpace()`](#calculatesubgraphrenderboundsouterspace)|
|[`CancelSynchronizedInput()`](#cancelsynchronizedinput)|
|[`ChangeLogicalParent(DependencyObject)`](#changelogicalparentdependencyobject)|
|[`ChangeSubtreeHasLoadedChangedHandler(DependencyObject)`](#changesubtreehasloadedchangedhandlerdependencyobject)|
|[`ChangeValidationVisualState(Boolean)`](#changevalidationvisualstateboolean)|
|[`ChangeVisualClip(Geometry,Boolean)`](#changevisualclipgeometryboolean)|
|[`ChangeVisualState(Boolean)`](#changevisualstateboolean)|
|[`CheckFlagsAnd(Channel,VisualProxyFlags)`](#checkflagsandchannelvisualproxyflags)|
|[`CheckFlagsAnd(VisualFlags)`](#checkflagsandvisualflags)|
|[`CheckFlagsOnAllChannels(VisualProxyFlags)`](#checkflagsonallchannelsvisualproxyflags)|
|[`CheckFlagsOr(Channel,VisualProxyFlags)`](#checkflagsorchannelvisualproxyflags)|
|[`CheckFlagsOr(VisualFlags)`](#checkflagsorvisualflags)|
|[`ClipChanged(Object,EventArgs)`](#clipchangedobjecteventargs)|
|[`ContainsValue(DependencyProperty)`](#containsvaluedependencyproperty)|
|[`ContentsChanged(Object,EventArgs)`](#contentschangedobjecteventargs)|
|[`ContextVerifiedGetParent()`](#contextverifiedgetparent)|
|[`CreateAutomationPeer()`](#createautomationpeer)|
|[`CreateGenericRootAutomationPeer()`](#creategenericrootautomationpeer)|
|[`Debug_AssertNoInheritanceContextListeners()`](#debug_assertnoinheritancecontextlisteners)|
|[`DetachFromDispatcher()`](#detachfromdispatcher)|
|[`DisconnectAttachedResource(VisualProxyFlags,IResource)`](#disconnectattachedresourcevisualproxyflagsiresource)|
|[`Dispose(Boolean)`](#disposeboolean)|
|[`Draw(RectangleShape,OverlayRefreshType)`](#drawrectangleshapeoverlayrefreshtype)|
|[`DrawCore(RectangleShape,OverlayRefreshType)`](#drawcorerectangleshapeoverlayrefreshtype)|
|[`EffectChanged(Object,EventArgs)`](#effectchangedobjecteventargs)|
|[`EndPropertyInitialization()`](#endpropertyinitialization)|
|[`EnsureEventHandlersStore()`](#ensureeventhandlersstore)|
|[`Enter()`](#enter)|
|[`EvaluateAnimatedValueCore(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`](#evaluateanimatedvaluecoredependencypropertypropertymetadataeffectivevalueentry&)|
|[`EvaluateBaseValueCore(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`](#evaluatebasevaluecoredependencypropertypropertymetadataeffectivevalueentry&)|
|[`EventHandlersStoreAdd(EventPrivateKey,Delegate)`](#eventhandlersstoreaddeventprivatekeydelegate)|
|[`EventHandlersStoreRemove(EventPrivateKey,Delegate)`](#eventhandlersstoreremoveeventprivatekeydelegate)|
|[`Exit()`](#exit)|
|[`Finalize()`](#finalize)|
|[`FindFirstAncestorWithFlagsAnd(VisualFlags)`](#findfirstancestorwithflagsandvisualflags)|
|[`FindName(String,DependencyObject&)`](#findnamestringdependencyobject&)|
|[`FindResourceOnSelf(Object,Boolean,Boolean)`](#findresourceonselfobjectbooleanboolean)|
|[`FireLoadedOnDescendentsInternal()`](#fireloadedondescendentsinternal)|
|[`FireOnVisualParentChanged(DependencyObject)`](#fireonvisualparentchangeddependencyobject)|
|[`FireUnloadedOnDescendentsInternal()`](#fireunloadedondescendentsinternal)|
|[`FreeContent(Channel)`](#freecontentchannel)|
|[`GetAutomationPeer()`](#getautomationpeer)|
|[`GetContentBounds()`](#getcontentbounds)|
|[`GetDpi()`](#getdpi)|
|[`GetDrawing()`](#getdrawing)|
|[`GetExpressionCore(DependencyProperty,PropertyMetadata)`](#getexpressioncoredependencypropertypropertymetadata)|
|[`GetHitTestBounds()`](#gethittestbounds)|
|[`GetLayoutClip(Size)`](#getlayoutclipsize)|
|[`GetLayoutClipInternal()`](#getlayoutclipinternal)|
|[`GetPlainText()`](#getplaintext)|
|[`GetRawValue(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`](#getrawvaluedependencypropertypropertymetadataeffectivevalueentry&)|
|[`GetTemplateChild(String)`](#gettemplatechildstring)|
|[`GetUIParent()`](#getuiparent)|
|[`GetUIParent(Boolean)`](#getuiparentboolean)|
|[`GetUIParentCore()`](#getuiparentcore)|
|[`GetUIParentNo3DTraversal()`](#getuiparentno3dtraversal)|
|[`GetUIParentOrICH(UIElement&,IContentHost&)`](#getuiparentorichuielement&icontenthost&)|
|[`GetUIParentWithinLayoutIsland()`](#getuiparentwithinlayoutisland)|
|[`GetValueEntry(EntryIndex,DependencyProperty,PropertyMetadata,RequestFlags)`](#getvalueentryentryindexdependencypropertypropertymetadatarequestflags)|
|[`GetValueSource(DependencyProperty,PropertyMetadata,Boolean&)`](#getvaluesourcedependencypropertypropertymetadataboolean&)|
|[`GetValueSource(DependencyProperty,PropertyMetadata,Boolean&,Boolean&,Boolean&,Boolean&,Boolean&)`](#getvaluesourcedependencypropertypropertymetadataboolean&boolean&boolean&boolean&boolean&)|
|[`GetVisualChild(Int32)`](#getvisualchildint32)|
|[`GetZoomLevelScale(Int32)`](#getzoomlevelscaleint32)|
|[`GuidelinesChanged(Object,EventArgs)`](#guidelineschangedobjecteventargs)|
|[`HasAnyExpression()`](#hasanyexpression)|
|[`HasExpression(EntryIndex,DependencyProperty)`](#hasexpressionentryindexdependencyproperty)|
|[`HasNonDefaultValue(DependencyProperty)`](#hasnondefaultvaluedependencyproperty)|
|[`HitTest(Point)`](#hittestpoint)|
|[`HitTest(Point,Boolean)`](#hittestpointboolean)|
|[`HitTest(HitTestFilterCallback,HitTestResultCallback,HitTestParameters)`](#hittesthittestfiltercallbackhittestresultcallbackhittestparameters)|
|[`HitTestCore(PointHitTestParameters)`](#hittestcorepointhittestparameters)|
|[`HitTestCore(GeometryHitTestParameters)`](#hittestcoregeometryhittestparameters)|
|[`HitTestGeometry(HitTestFilterCallback,HitTestResultCallback,GeometryHitTestParameters)`](#hittestgeometryhittestfiltercallbackhittestresultcallbackgeometryhittestparameters)|
|[`HitTestPoint(HitTestFilterCallback,HitTestResultCallback,PointHitTestParameters)`](#hittestpointhittestfiltercallbackhittestresultcallbackpointhittestparameters)|
|[`HitTestPointInternal(HitTestFilterCallback,HitTestResultCallback,PointHitTestParameters)`](#hittestpointinternalhittestfiltercallbackhittestresultcallbackpointhittestparameters)|
|[`IgnoreModelParentBuildRoute(RoutedEventArgs)`](#ignoremodelparentbuildrouteroutedeventargs)|
|[`InputHitTest(Point,IInputElement&,IInputElement&)`](#inputhittestpointiinputelement&iinputelement&)|
|[`InputHitTest(Point,IInputElement&,IInputElement&,HitTestResult&)`](#inputhittestpointiinputelement&iinputelement&hittestresult&)|
|[`InternalAddVisualChild(Visual)`](#internaladdvisualchildvisual)|
|[`InternalGet2DOr3DVisualChild(Int32)`](#internalget2dor3dvisualchildint32)|
|[`InternalGetVisualChild(Int32)`](#internalgetvisualchildint32)|
|[`InternalRemoveVisualChild(Visual)`](#internalremovevisualchildvisual)|
|[`InternalSetOffsetWorkaround(Vector)`](#internalsetoffsetworkaroundvector)|
|[`InternalSetTransformWorkaround(Transform)`](#internalsettransformworkaroundtransform)|
|[`InvalidateArrangeInternal()`](#invalidatearrangeinternal)|
|[`InvalidateAutomationAncestorsCore(Stack<DependencyObject>,Boolean&)`](#invalidateautomationancestorscorestack<dependencyobject>boolean&)|
|[`InvalidateAutomationAncestorsCoreHelper(Stack<DependencyObject>,Boolean&,Boolean)`](#invalidateautomationancestorscorehelperstack<dependencyobject>boolean&boolean)|
|[`InvalidateForceInheritPropertyOnChildren(DependencyProperty)`](#invalidateforceinheritpropertyonchildrendependencyproperty)|
|[`InvalidateHitTestBounds()`](#invalidatehittestbounds)|
|[`InvalidateMeasureInternal()`](#invalidatemeasureinternal)|
|[`InvalidateProperty(DependencyProperty,Boolean)`](#invalidatepropertydependencypropertyboolean)|
|[`InvalidateSubProperty(DependencyProperty)`](#invalidatesubpropertydependencyproperty)|
|[`InvalidateTreeDependentProperties(TreeChangeInfo,Boolean,Boolean)`](#invalidatetreedependentpropertiestreechangeinfobooleanboolean)|
|[`InvalidateZOrder()`](#invalidatezorder)|
|[`InvokeAccessKey(AccessKeyEventArgs)`](#invokeaccesskeyaccesskeyeventargs)|
|[`IsOnChannel(Channel)`](#isonchannelchannel)|
|[`LoadStateCore(Byte[])`](#loadstatecorebyte[])|
|[`LookupEntry(Int32)`](#lookupentryint32)|
|[`MakeSentinel()`](#makesentinel)|
|[`MeasureCore(Size)`](#measurecoresize)|
|[`MeasureOverride(Size)`](#measureoverridesize)|
|[`MemberwiseClone()`](#memberwiseclone)|
|[`NotifyPropertyChange(DependencyPropertyChangedEventArgs)`](#notifypropertychangedependencypropertychangedeventargs)|
|[`NotifySubPropertyChange(DependencyProperty)`](#notifysubpropertychangedependencyproperty)|
|[`OnAccessKey(AccessKeyEventArgs)`](#onaccesskeyaccesskeyeventargs)|
|[`OnAddHandler(RoutedEvent,Delegate)`](#onaddhandlerroutedeventdelegate)|
|[`OnAncestorChanged()`](#onancestorchanged)|
|[`OnAncestorChangedInternal(TreeChangeInfo)`](#onancestorchangedinternaltreechangeinfo)|
|[`OnChildDesiredSizeChanged(UIElement)`](#onchilddesiredsizechangeduielement)|
|[`OnContextMenuClosing(ContextMenuEventArgs)`](#oncontextmenuclosingcontextmenueventargs)|
|[`OnContextMenuOpening(ContextMenuEventArgs)`](#oncontextmenuopeningcontextmenueventargs)|
|[`OnCreateAutomationPeer()`](#oncreateautomationpeer)|
|[`OnCreateAutomationPeerInternal()`](#oncreateautomationpeerinternal)|
|[`OnCurrentExtentChanged(CurrentExtentChangedMapViewEventArgs)`](#oncurrentextentchangedcurrentextentchangedmapvieweventargs)|
|[`OnCurrentExtentChanging(CurrentExtentChangingMapViewEventArgs)`](#oncurrentextentchangingcurrentextentchangingmapvieweventargs)|
|[`OnCurrentScaleChanged(CurrentScaleChangedMapViewEventArgs)`](#oncurrentscalechangedcurrentscalechangedmapvieweventargs)|
|[`OnCurrentScaleChanging(CurrentScaleChangingMapViewEventArgs)`](#oncurrentscalechangingcurrentscalechangingmapvieweventargs)|
|[`OnDpiChanged(DpiScale,DpiScale)`](#ondpichangeddpiscaledpiscale)|
|[`OnDragEnter(DragEventArgs)`](#ondragenterdrageventargs)|
|[`OnDragLeave(DragEventArgs)`](#ondragleavedrageventargs)|
|[`OnDragOver(DragEventArgs)`](#ondragoverdrageventargs)|
|[`OnDrop(DragEventArgs)`](#ondropdrageventargs)|
|[`OnGiveFeedback(GiveFeedbackEventArgs)`](#ongivefeedbackgivefeedbackeventargs)|
|[`OnGotFocus(RoutedEventArgs)`](#ongotfocusroutedeventargs)|
|[`OnGotKeyboardFocus(KeyboardFocusChangedEventArgs)`](#ongotkeyboardfocuskeyboardfocuschangedeventargs)|
|[`OnGotMouseCapture(MouseEventArgs)`](#ongotmousecapturemouseeventargs)|
|[`OnGotStylusCapture(StylusEventArgs)`](#ongotstyluscapturestyluseventargs)|
|[`OnGotTouchCapture(TouchEventArgs)`](#ongottouchcapturetoucheventargs)|
|[`OnInheritanceContextChanged(EventArgs)`](#oninheritancecontextchangedeventargs)|
|[`OnInheritanceContextChangedCore(EventArgs)`](#oninheritancecontextchangedcoreeventargs)|
|[`OnInitialized(EventArgs)`](#oninitializedeventargs)|
|[`OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs)`](#oniskeyboardfocusedchangeddependencypropertychangedeventargs)|
|[`OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs)`](#oniskeyboardfocuswithinchangeddependencypropertychangedeventargs)|
|[`OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs)`](#onismousecapturedchangeddependencypropertychangedeventargs)|
|[`OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs)`](#onismousecapturewithinchangeddependencypropertychangedeventargs)|
|[`OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs)`](#onismousedirectlyoverchangeddependencypropertychangedeventargs)|
|[`OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs)`](#onisstyluscapturedchangeddependencypropertychangedeventargs)|
|[`OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs)`](#onisstyluscapturewithinchangeddependencypropertychangedeventargs)|
|[`OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs)`](#onisstylusdirectlyoverchangeddependencypropertychangedeventargs)|
|[`OnKeyDown(KeyEventArgs)`](#onkeydownkeyeventargs)|
|[`OnKeyUp(KeyEventArgs)`](#onkeyupkeyeventargs)|
|[`OnLoaded(RoutedEventArgs)`](#onloadedroutedeventargs)|
|[`OnLostFocus(RoutedEventArgs)`](#onlostfocusroutedeventargs)|
|[`OnLostKeyboardFocus(KeyboardFocusChangedEventArgs)`](#onlostkeyboardfocuskeyboardfocuschangedeventargs)|
|[`OnLostMouseCapture(MouseEventArgs)`](#onlostmousecapturemouseeventargs)|
|[`OnLostStylusCapture(StylusEventArgs)`](#onloststyluscapturestyluseventargs)|
|[`OnLostTouchCapture(TouchEventArgs)`](#onlosttouchcapturetoucheventargs)|
|[`OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs)`](#onmanipulationboundaryfeedbackmanipulationboundaryfeedbackeventargs)|
|[`OnManipulationCompleted(ManipulationCompletedEventArgs)`](#onmanipulationcompletedmanipulationcompletedeventargs)|
|[`OnManipulationDelta(ManipulationDeltaEventArgs)`](#onmanipulationdeltamanipulationdeltaeventargs)|
|[`OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs)`](#onmanipulationinertiastartingmanipulationinertiastartingeventargs)|
|[`OnManipulationStarted(ManipulationStartedEventArgs)`](#onmanipulationstartedmanipulationstartedeventargs)|
|[`OnManipulationStarting(ManipulationStartingEventArgs)`](#onmanipulationstartingmanipulationstartingeventargs)|
|[`OnMapClick(MapClickMapViewEventArgs)`](#onmapclickmapclickmapvieweventargs)|
|[`OnMapDoubleClick(MapClickMapViewEventArgs)`](#onmapdoubleclickmapclickmapvieweventargs)|
|[`OnMapTap(MapTapMapViewEventArgs)`](#onmaptapmaptapmapvieweventargs)|
|[`OnMouseDoubleClick(MouseButtonEventArgs)`](#onmousedoubleclickmousebuttoneventargs)|
|[`OnMouseDown(MouseButtonEventArgs)`](#onmousedownmousebuttoneventargs)|
|[`OnMouseEnter(MouseEventArgs)`](#onmouseentermouseeventargs)|
|[`OnMouseLeave(MouseEventArgs)`](#onmouseleavemouseeventargs)|
|[`OnMouseLeftButtonDown(MouseButtonEventArgs)`](#onmouseleftbuttondownmousebuttoneventargs)|
|[`OnMouseLeftButtonUp(MouseButtonEventArgs)`](#onmouseleftbuttonupmousebuttoneventargs)|
|[`OnMouseMove(MouseEventArgs)`](#onmousemovemouseeventargs)|
|[`OnMouseRightButtonDown(MouseButtonEventArgs)`](#onmouserightbuttondownmousebuttoneventargs)|
|[`OnMouseRightButtonUp(MouseButtonEventArgs)`](#onmouserightbuttonupmousebuttoneventargs)|
|[`OnMouseUp(MouseButtonEventArgs)`](#onmouseupmousebuttoneventargs)|
|[`OnMouseWheel(MouseWheelEventArgs)`](#onmousewheelmousewheeleventargs)|
|[`OnNewParent(DependencyObject)`](#onnewparentdependencyobject)|
|[`OnOverlayDrawing(OverlayDrawingMapViewEventArgs)`](#onoverlaydrawingoverlaydrawingmapvieweventargs)|
|[`OnOverlayDrawn(OverlayDrawnMapViewEventArgs)`](#onoverlaydrawnoverlaydrawnmapvieweventargs)|
|[`OnOverlaysDrawing(OverlaysDrawingMapViewEventArgs)`](#onoverlaysdrawingoverlaysdrawingmapvieweventargs)|
|[`OnOverlaysDrawn(OverlaysDrawnMapViewEventArgs)`](#onoverlaysdrawnoverlaysdrawnmapvieweventargs)|
|[`OnPostApplyTemplate()`](#onpostapplytemplate)|
|[`OnPreApplyTemplate()`](#onpreapplytemplate)|
|[`OnPresentationSourceChanged(Boolean)`](#onpresentationsourcechangedboolean)|
|[`OnPreviewDragEnter(DragEventArgs)`](#onpreviewdragenterdrageventargs)|
|[`OnPreviewDragLeave(DragEventArgs)`](#onpreviewdragleavedrageventargs)|
|[`OnPreviewDragOver(DragEventArgs)`](#onpreviewdragoverdrageventargs)|
|[`OnPreviewDrop(DragEventArgs)`](#onpreviewdropdrageventargs)|
|[`OnPreviewGiveFeedback(GiveFeedbackEventArgs)`](#onpreviewgivefeedbackgivefeedbackeventargs)|
|[`OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs)`](#onpreviewgotkeyboardfocuskeyboardfocuschangedeventargs)|
|[`OnPreviewKeyDown(KeyEventArgs)`](#onpreviewkeydownkeyeventargs)|
|[`OnPreviewKeyUp(KeyEventArgs)`](#onpreviewkeyupkeyeventargs)|
|[`OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs)`](#onpreviewlostkeyboardfocuskeyboardfocuschangedeventargs)|
|[`OnPreviewMouseDoubleClick(MouseButtonEventArgs)`](#onpreviewmousedoubleclickmousebuttoneventargs)|
|[`OnPreviewMouseDown(MouseButtonEventArgs)`](#onpreviewmousedownmousebuttoneventargs)|
|[`OnPreviewMouseLeftButtonDown(MouseButtonEventArgs)`](#onpreviewmouseleftbuttondownmousebuttoneventargs)|
|[`OnPreviewMouseLeftButtonUp(MouseButtonEventArgs)`](#onpreviewmouseleftbuttonupmousebuttoneventargs)|
|[`OnPreviewMouseMove(MouseEventArgs)`](#onpreviewmousemovemouseeventargs)|
|[`OnPreviewMouseRightButtonDown(MouseButtonEventArgs)`](#onpreviewmouserightbuttondownmousebuttoneventargs)|
|[`OnPreviewMouseRightButtonUp(MouseButtonEventArgs)`](#onpreviewmouserightbuttonupmousebuttoneventargs)|
|[`OnPreviewMouseUp(MouseButtonEventArgs)`](#onpreviewmouseupmousebuttoneventargs)|
|[`OnPreviewMouseWheel(MouseWheelEventArgs)`](#onpreviewmousewheelmousewheeleventargs)|
|[`OnPreviewQueryContinueDrag(QueryContinueDragEventArgs)`](#onpreviewquerycontinuedragquerycontinuedrageventargs)|
|[`OnPreviewStylusButtonDown(StylusButtonEventArgs)`](#onpreviewstylusbuttondownstylusbuttoneventargs)|
|[`OnPreviewStylusButtonUp(StylusButtonEventArgs)`](#onpreviewstylusbuttonupstylusbuttoneventargs)|
|[`OnPreviewStylusDown(StylusDownEventArgs)`](#onpreviewstylusdownstylusdowneventargs)|
|[`OnPreviewStylusInAirMove(StylusEventArgs)`](#onpreviewstylusinairmovestyluseventargs)|
|[`OnPreviewStylusInRange(StylusEventArgs)`](#onpreviewstylusinrangestyluseventargs)|
|[`OnPreviewStylusMove(StylusEventArgs)`](#onpreviewstylusmovestyluseventargs)|
|[`OnPreviewStylusOutOfRange(StylusEventArgs)`](#onpreviewstylusoutofrangestyluseventargs)|
|[`OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs)`](#onpreviewstylussystemgesturestylussystemgestureeventargs)|
|[`OnPreviewStylusUp(StylusEventArgs)`](#onpreviewstylusupstyluseventargs)|
|[`OnPreviewTextInput(TextCompositionEventArgs)`](#onpreviewtextinputtextcompositioneventargs)|
|[`OnPreviewTouchDown(TouchEventArgs)`](#onpreviewtouchdowntoucheventargs)|
|[`OnPreviewTouchMove(TouchEventArgs)`](#onpreviewtouchmovetoucheventargs)|
|[`OnPreviewTouchUp(TouchEventArgs)`](#onpreviewtouchuptoucheventargs)|
|[`OnPropertyChanged(DependencyPropertyChangedEventArgs)`](#onpropertychangeddependencypropertychangedeventargs)|
|[`OnQueryContinueDrag(QueryContinueDragEventArgs)`](#onquerycontinuedragquerycontinuedrageventargs)|
|[`OnQueryCursor(QueryCursorEventArgs)`](#onquerycursorquerycursoreventargs)|
|[`OnRemoveHandler(RoutedEvent,Delegate)`](#onremovehandlerroutedeventdelegate)|
|[`OnRender(DrawingContext)`](#onrenderdrawingcontext)|
|[`OnRenderSizeChanged(SizeChangedInfo)`](#onrendersizechangedsizechangedinfo)|
|[`OnStyleChanged(Style,Style)`](#onstylechangedstylestyle)|
|[`OnStylusButtonDown(StylusButtonEventArgs)`](#onstylusbuttondownstylusbuttoneventargs)|
|[`OnStylusButtonUp(StylusButtonEventArgs)`](#onstylusbuttonupstylusbuttoneventargs)|
|[`OnStylusDown(StylusDownEventArgs)`](#onstylusdownstylusdowneventargs)|
|[`OnStylusEnter(StylusEventArgs)`](#onstylusenterstyluseventargs)|
|[`OnStylusInAirMove(StylusEventArgs)`](#onstylusinairmovestyluseventargs)|
|[`OnStylusInRange(StylusEventArgs)`](#onstylusinrangestyluseventargs)|
|[`OnStylusLeave(StylusEventArgs)`](#onstylusleavestyluseventargs)|
|[`OnStylusMove(StylusEventArgs)`](#onstylusmovestyluseventargs)|
|[`OnStylusOutOfRange(StylusEventArgs)`](#onstylusoutofrangestyluseventargs)|
|[`OnStylusSystemGesture(StylusSystemGestureEventArgs)`](#onstylussystemgesturestylussystemgestureeventargs)|
|[`OnStylusUp(StylusEventArgs)`](#onstylusupstyluseventargs)|
|[`OnTemplateChanged(ControlTemplate,ControlTemplate)`](#ontemplatechangedcontroltemplatecontroltemplate)|
|[`OnTemplateChangedInternal(FrameworkTemplate,FrameworkTemplate)`](#ontemplatechangedinternalframeworktemplateframeworktemplate)|
|[`OnTextInput(TextCompositionEventArgs)`](#ontextinputtextcompositioneventargs)|
|[`OnThemeChanged()`](#onthemechanged)|
|[`OnToolTipClosing(ToolTipEventArgs)`](#ontooltipclosingtooltipeventargs)|
|[`OnToolTipOpening(ToolTipEventArgs)`](#ontooltipopeningtooltipeventargs)|
|[`OnTouchDown(TouchEventArgs)`](#ontouchdowntoucheventargs)|
|[`OnTouchEnter(TouchEventArgs)`](#ontouchentertoucheventargs)|
|[`OnTouchLeave(TouchEventArgs)`](#ontouchleavetoucheventargs)|
|[`OnTouchMove(TouchEventArgs)`](#ontouchmovetoucheventargs)|
|[`OnTouchUp(TouchEventArgs)`](#ontouchuptoucheventargs)|
|[`OnUnloaded(RoutedEventArgs)`](#onunloadedroutedeventargs)|
|[`OnVisualAncestorChanged(Object,AncestorChangedEventArgs)`](#onvisualancestorchangedobjectancestorchangedeventargs)|
|[`OnVisualAncestorChanged(Object,AncestorChangedEventArgs)`](#onvisualancestorchangedobjectancestorchangedeventargs)|
|[`OnVisualChildrenChanged(DependencyObject,DependencyObject)`](#onvisualchildrenchangeddependencyobjectdependencyobject)|
|[`OnVisualParentChanged(DependencyObject)`](#onvisualparentchangeddependencyobject)|
|[`OnZoomLevelSetChanged(ZoomLevelSetChangedMapViewEventArgs)`](#onzoomlevelsetchangedzoomlevelsetchangedmapvieweventargs)|
|[`OpacityMaskChanged(Object,EventArgs)`](#opacitymaskchangedobjecteventargs)|
|[`ParentLayoutInvalidated(UIElement)`](#parentlayoutinvalidateduielement)|
|[`Precompute()`](#precompute)|
|[`PrecomputeContent()`](#precomputecontent)|
|[`PrecomputeRecursive(Rect&)`](#precomputerecursiverect&)|
|[`PropagateChangedFlags()`](#propagatechangedflags)|
|[`ProvideSelfAsInheritanceContext(Object,DependencyProperty)`](#provideselfasinheritancecontextobjectdependencyproperty)|
|[`ProvideSelfAsInheritanceContext(DependencyObject,DependencyProperty)`](#provideselfasinheritancecontextdependencyobjectdependencyproperty)|
|[`pushTextRenderingMode()`](#pushtextrenderingmode)|
|[`RaiseClrEvent(EventPrivateKey,EventArgs)`](#raiseclreventeventprivatekeyeventargs)|
|[`RaiseEvent(RoutedEventArgs,Boolean)`](#raiseeventroutedeventargsboolean)|
|[`RaiseInheritedPropertyChangedEvent(InheritablePropertyChangeInfo&)`](#raiseinheritedpropertychangedeventinheritablepropertychangeinfo&)|
|[`RaiseIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs)`](#raiseiskeyboardfocuswithinchangeddependencypropertychangedeventargs)|
|[`RaiseIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs)`](#raiseismousecapturewithinchangeddependencypropertychangedeventargs)|
|[`RaiseIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs)`](#raiseisstyluscapturewithinchangeddependencypropertychangedeventargs)|
|[`RaiseTrustedEvent(RoutedEventArgs)`](#raisetrustedeventroutedeventargs)|
|[`ReadControlFlag(ControlBoolFlags)`](#readcontrolflagcontrolboolflags)|
|[`ReadFlag(CoreFlags)`](#readflagcoreflags)|
|[`ReadInternalFlag(InternalFlags)`](#readinternalflaginternalflags)|
|[`ReadInternalFlag2(InternalFlags2)`](#readinternalflag2internalflags2)|
|[`ReadLocalValueEntry(EntryIndex,DependencyProperty,Boolean)`](#readlocalvalueentryentryindexdependencypropertyboolean)|
|[`RecursiveSetDpiScaleVisualFlags(DpiRecursiveChangeArgs)`](#recursivesetdpiscalevisualflagsdpirecursivechangeargs)|
|[`ReleaseOnChannelCore(Channel)`](#releaseonchannelcorechannel)|
|[`ReleaseOnChannelForCyclicBrush(ICyclicBrush,Channel)`](#releaseonchannelforcyclicbrushicyclicbrushchannel)|
|[`RemoveInheritanceContext(DependencyObject,DependencyProperty)`](#removeinheritancecontextdependencyobjectdependencyproperty)|
|[`RemoveLogicalChild(Object)`](#removelogicalchildobject)|
|[`RemoveSelfAsInheritanceContext(Object,DependencyProperty)`](#removeselfasinheritancecontextobjectdependencyproperty)|
|[`RemoveSelfAsInheritanceContext(DependencyObject,DependencyProperty)`](#removeselfasinheritancecontextdependencyobjectdependencyproperty)|
|[`RemoveVisualChild(Visual)`](#removevisualchildvisual)|
|[`Render(RenderContext,UInt32)`](#renderrendercontextuint32)|
|[`RenderClose(IDrawingContent)`](#rendercloseidrawingcontent)|
|[`RenderContent(RenderContext,Boolean)`](#rendercontentrendercontextboolean)|
|[`RenderOpen()`](#renderopen)|
|[`RenderRecursive(RenderContext)`](#renderrecursiverendercontext)|
|[`SaveStateCore()`](#savestatecore)|
|[`ScrollableAreaClipChanged(Object,EventArgs)`](#scrollableareaclipchangedobjecteventargs)|
|[`Seal()`](#seal)|
|[`SetCurrentDeferredValue(DependencyProperty,DeferredReference)`](#setcurrentdeferredvaluedependencypropertydeferredreference)|
|[`SetCurrentValue(DependencyProperty,Boolean)`](#setcurrentvaluedependencypropertyboolean)|
|[`SetCurrentValueInternal(DependencyProperty,Object)`](#setcurrentvalueinternaldependencypropertyobject)|
|[`SetDeferredValue(DependencyProperty,DeferredReference)`](#setdeferredvaluedependencypropertydeferredreference)|
|[`SetDpiScaleVisualFlags(DpiRecursiveChangeArgs)`](#setdpiscalevisualflagsdpirecursivechangeargs)|
|[`SetEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata,EffectiveValueEntry,EffectiveValueEntry)`](#seteffectivevalueentryindexdependencypropertypropertymetadataeffectivevalueentryeffectivevalueentry)|
|[`SetEffectiveValue(EntryIndex,DependencyProperty,Int32,PropertyMetadata,Object,BaseValueSourceInternal)`](#seteffectivevalueentryindexdependencypropertyint32propertymetadataobjectbasevaluesourceinternal)|
|[`SetFlags(Channel,Boolean,VisualProxyFlags)`](#setflagschannelbooleanvisualproxyflags)|
|[`SetFlags(Boolean,VisualFlags)`](#setflagsbooleanvisualflags)|
|[`SetFlagsOnAllChannels(Boolean,VisualProxyFlags)`](#setflagsonallchannelsbooleanvisualproxyflags)|
|[`SetFlagsToRoot(Boolean,VisualFlags)`](#setflagstorootbooleanvisualflags)|
|[`SetIsSelfInheritanceParent()`](#setisselfinheritanceparent)|
|[`SetMutableDefaultValue(DependencyProperty,Object)`](#setmutabledefaultvaluedependencypropertyobject)|
|[`SetPersistId(Int32)`](#setpersistidint32)|
|[`SetValue(DependencyProperty,Boolean)`](#setvaluedependencypropertyboolean)|
|[`SetValue(DependencyPropertyKey,Boolean)`](#setvaluedependencypropertykeyboolean)|
|[`SetValueInternal(DependencyProperty,Object)`](#setvalueinternaldependencypropertyobject)|
|[`ShouldProvideInheritanceContext(DependencyObject,DependencyProperty)`](#shouldprovideinheritancecontextdependencyobjectdependencyproperty)|
|[`ShouldSerializeProperty(DependencyProperty)`](#shouldserializepropertydependencyproperty)|
|[`StartListeningSynchronizedInput(SynchronizedInputType)`](#startlisteningsynchronizedinputsynchronizedinputtype)|
|[`SynchronizedInputPostOpportunityHandler(Object,RoutedEventArgs)`](#synchronizedinputpostopportunityhandlerobjectroutedeventargs)|
|[`SynchronizedInputPreOpportunityHandler(Object,RoutedEventArgs)`](#synchronizedinputpreopportunityhandlerobjectroutedeventargs)|
|[`SynchronizeInheritanceParent(DependencyObject)`](#synchronizeinheritanceparentdependencyobject)|
|[`SynchronizeReverseInheritPropertyFlags(DependencyObject,Boolean)`](#synchronizereverseinheritpropertyflagsdependencyobjectboolean)|
|[`TransformChanged(Object,EventArgs)`](#transformchangedobjecteventargs)|
|[`TransformToOuterSpace()`](#transformtoouterspace)|
|[`TrySimpleTransformToAncestor(Visual,Boolean,GeneralTransform&,Matrix&)`](#trysimpletransformtoancestorvisualbooleangeneraltransform&matrix&)|
|[`TrySimpleTransformToAncestor(Visual3D,GeneralTransform2DTo3D&)`](#trysimpletransformtoancestorvisual3dgeneraltransform2dto3d&)|
|[`UnsetEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata)`](#unseteffectivevalueentryindexdependencypropertypropertymetadata)|
|[`UpdateEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata,EffectiveValueEntry,EffectiveValueEntry&,Boolean,Boolean,OperationType)`](#updateeffectivevalueentryindexdependencypropertypropertymetadataeffectivevalueentryeffectivevalueentry&booleanbooleanoperationtype)|
|[`UpdateIsVisibleCache()`](#updateisvisiblecache)|
|[`UpdateStyleProperty()`](#updatestyleproperty)|
|[`UpdateThemeStyleProperty()`](#updatethemestyleproperty)|
|[`UpdateVisualState()`](#updatevisualstate)|
|[`UpdateVisualState(Boolean)`](#updatevisualstateboolean)|
|[`VerifyAPIReadOnly()`](#verifyapireadonly)|
|[`VerifyAPIReadOnly(DependencyObject)`](#verifyapireadonlydependencyobject)|
|[`VerifyAPIReadWrite()`](#verifyapireadwrite)|
|[`VerifyAPIReadWrite(DependencyObject)`](#verifyapireadwritedependencyobject)|
|[`WalkContent(DrawingContextWalker)`](#walkcontentdrawingcontextwalker)|
|[`WriteControlFlag(ControlBoolFlags,Boolean)`](#writecontrolflagcontrolboolflagsboolean)|
|[`WriteFlag(CoreFlags,Boolean)`](#writeflagcoreflagsboolean)|
|[`WriteInternalFlag(InternalFlags,Boolean)`](#writeinternalflaginternalflagsboolean)|
|[`WriteInternalFlag2(InternalFlags2,Boolean)`](#writeinternalflag2internalflags2boolean)|
|[`ZoomToPreviousExtentCore()`](#zoomtopreviousextentcore)|

### Public Events Summary


|Name|Event Arguments|Description|
|---|---|---|
|[`MapClick`](#mapclick)|[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)|N/A|
|[`MapTap`](#maptap)|[`MapTapMapViewEventArgs`](ThinkGeo.UI.Wpf.MapTapMapViewEventArgs.md)|N/A|
|[`MapDoubleClick`](#mapdoubleclick)|[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)|N/A|
|[`CurrentExtentChanging`](#currentextentchanging)|[`CurrentExtentChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangingMapViewEventArgs.md)|N/A|
|[`ZoomLevelSetChanged`](#zoomlevelsetchanged)|[`ZoomLevelSetChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.ZoomLevelSetChangedMapViewEventArgs.md)|N/A|
|[`CurrentExtentChanged`](#currentextentchanged)|[`CurrentExtentChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangedMapViewEventArgs.md)|N/A|
|[`CurrentScaleChanging`](#currentscalechanging)|[`CurrentScaleChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangingMapViewEventArgs.md)|N/A|
|[`CurrentScaleChanged`](#currentscalechanged)|[`CurrentScaleChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangedMapViewEventArgs.md)|N/A|
|[`OverlayDrawing`](#overlaydrawing)|[`OverlayDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawingMapViewEventArgs.md)|N/A|
|[`OverlayDrawn`](#overlaydrawn)|[`OverlayDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawnMapViewEventArgs.md)|N/A|
|[`OverlaysDrawing`](#overlaysdrawing)|[`OverlaysDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawingMapViewEventArgs.md)|N/A|
|[`OverlaysDrawn`](#overlaysdrawn)|[`OverlaysDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawnMapViewEventArgs.md)|N/A|
|[`TargetUpdated`](#targetupdated)|`DataTransferEventArgs`|N/A|
|[`SourceUpdated`](#sourceupdated)|`DataTransferEventArgs`|N/A|
|[`PreviewTouchDown`](#previewtouchdown)|`TouchEventArgs`|N/A|
|[`TouchDown`](#touchdown)|`TouchEventArgs`|N/A|
|[`PreviewTouchMove`](#previewtouchmove)|`TouchEventArgs`|N/A|
|[`TouchMove`](#touchmove)|`TouchEventArgs`|N/A|
|[`PreviewTouchUp`](#previewtouchup)|`TouchEventArgs`|N/A|
|[`TouchUp`](#touchup)|`TouchEventArgs`|N/A|
|[`GotTouchCapture`](#gottouchcapture)|`TouchEventArgs`|N/A|
|[`LostTouchCapture`](#losttouchcapture)|`TouchEventArgs`|N/A|
|[`TouchEnter`](#touchenter)|`TouchEventArgs`|N/A|
|[`TouchLeave`](#touchleave)|`TouchEventArgs`|N/A|
|[`ManipulationStarting`](#manipulationstarting)|`ManipulationStartingEventArgs`|N/A|
|[`ManipulationStarted`](#manipulationstarted)|`ManipulationStartedEventArgs`|N/A|
|[`ManipulationDelta`](#manipulationdelta)|`ManipulationDeltaEventArgs`|N/A|
|[`ManipulationInertiaStarting`](#manipulationinertiastarting)|`ManipulationInertiaStartingEventArgs`|N/A|
|[`ManipulationBoundaryFeedback`](#manipulationboundaryfeedback)|`ManipulationBoundaryFeedbackEventArgs`|N/A|
|[`ManipulationCompleted`](#manipulationcompleted)|`ManipulationCompletedEventArgs`|N/A|

## Members Detail

### Public Constructors


|Name|
|---|
|[`MapView()`](#mapview)|

### Protected Constructors


### Public Properties

#### `ActualHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `ActualWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `AdornmentOverlay`

**Summary**

   *Gets or sets the adornment overlay in the MapControl.*

**Remarks**

   *N/A*

**Return Value**

[`AdornmentOverlay`](ThinkGeo.UI.Wpf.AdornmentOverlay.md)

---
#### `AllowDrop`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AreAnyTouchesCaptured`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AreAnyTouchesCapturedWithin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AreAnyTouchesDirectlyOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AreAnyTouchesOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Background`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Brush`

---
#### `BackgroundOverlay`

**Summary**

   *Gets or sets the background overlay.*

**Remarks**

   *N/A*

**Return Value**

[`BackgroundOverlay`](ThinkGeo.UI.Wpf.BackgroundOverlay.md)

---
#### `BindingGroup`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BindingGroup`

---
#### `BitmapEffect`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffect`

---
#### `BitmapEffectInput`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffectInput`

---
#### `BorderBrush`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Brush`

---
#### `BorderThickness`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Thickness`

---
#### `CacheMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CacheMode`

---
#### `Clip`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Geometry`

---
#### `ClipToBounds`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CommandBindings`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CommandBindingCollection`

---
#### `ContextMenu`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`ContextMenu`

---
#### `CurrentExtent`

**Summary**

   *Gets or sets the current extent of the MapControl.*

**Remarks**

   *The current extent stands for the extent of current position, this is very important metrics to caculate the scale.*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `CurrentScale`

**Summary**

   *Gets or sets current viewport scale.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Cursor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Cursor`

---
#### `DataContext`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `DependencyObjectType`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObjectType`

---
#### `DesiredSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Size`

---
#### `Dispatcher`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Dispatcher`

---
#### `EditOverlay`

**Summary**

   *Gets or sets the edit overlay in the MapControl.*

**Remarks**

   *N/A*

**Return Value**

[`EditInteractiveOverlay`](ThinkGeo.UI.Wpf.EditInteractiveOverlay.md)

---
#### `Effect`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Effect`

---
#### `ExtentOverlay`

**Summary**

   *Gets or sets the extent overlay in the MapControl.*

**Remarks**

   *N/A*

**Return Value**

[`ExtentInteractiveOverlay`](ThinkGeo.UI.Wpf.ExtentInteractiveOverlay.md)

---
#### `FlowDirection`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FlowDirection`

---
#### `Focusable`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `FocusVisualStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Style`

---
#### `FontFamily`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FontFamily`

---
#### `FontSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `FontStretch`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FontStretch`

---
#### `FontStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FontStyle`

---
#### `FontWeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FontWeight`

---
#### `ForceCursor`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Foreground`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Brush`

---
#### `HasAnimatedProperties`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Height`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `HorizontalAlignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`HorizontalAlignment`

---
#### `HorizontalContentAlignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`HorizontalAlignment`

---
#### `InputBindings`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`InputBindingCollection`

---
#### `InputScope`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`InputScope`

---
#### `InteractiveOverlays`

**Summary**

   *This property gets the collection of InteractiveOverlays in the MapControl.*

**Remarks**

   *This property is used when you want to change the default behavior of the InteractiveOverlay or add your own customized InteractiveOverlay.*

**Return Value**

GeoCollection<[`InteractiveOverlay`](ThinkGeo.UI.Wpf.InteractiveOverlay.md)>

---
#### `IsArrangeValid`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsEnabled`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsFocused`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsHitTestVisible`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsInitialized`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsInputMethodEnabled`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsKeyboardFocused`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsKeyboardFocusWithin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsLoaded`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsManipulationEnabled`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsMeasureValid`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsMouseCaptured`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsMouseCaptureWithin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsMouseDirectlyOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsMouseOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsSealed`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStylusCaptured`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStylusCaptureWithin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStylusDirectlyOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStylusOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsTabStop`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsVisible`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Language`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`XmlLanguage`

---
#### `LayoutTransform`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Transform`

---
#### `MapResizeMode`

**Summary**

   *Gets a strategy for changing extent when resizes map control.*

**Remarks**

   *N/A*

**Return Value**

[`MapResizeMode`](ThinkGeo.UI.Wpf.MapResizeMode.md)

---
#### `MapTools`

**Summary**

   *Gets a object for simply using MapTools.*

**Remarks**

   *N/A*

**Return Value**

[`MapTools`](ThinkGeo.UI.Wpf.MapTools.md)

---
#### `MapUnit`

**Summary**

   *Gets or sets the map unit used by the MapControl.*

**Remarks**

   *The MapUnit reflects the data unit.*

**Return Value**

[`GeographyUnit`](../ThinkGeo.Core/ThinkGeo.Core.GeographyUnit.md)

---
#### `Margin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Thickness`

---
#### `MaxHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MaximumScale`

**Summary**

   *This property gets and sets a maximum scale when navigating the map. When you keep zooming out, and the target scale is bigger than the maximum scale, the zooming operation will be stopped.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MaxWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinHeight`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinimumScale`

**Summary**

   *This property gets and sets a minimum scale when navigating the map. When you keep zooming in, and the target scale is smaller than the minimum scale, the zooming operation will be stopped.*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `MinWidth`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `Name`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `Opacity`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `OpacityMask`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Brush`

---
#### `Overlays`

**Summary**

   *This property gets the collection of Overlays in the MapControl.*

**Remarks**

   *N/A*

**Return Value**

GeoCollection<[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)>

---
#### `OverridesDefaultStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Padding`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Thickness`

---
#### `Parent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `PersistId`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `PivotScreenPoint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)

---
#### `RenderSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Size`

---
#### `RenderTransform`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Transform`

---
#### `RenderTransformOrigin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Point`

---
#### `Resources`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`ResourceDictionary`

---
#### `RestrictExtent`

**Summary**

   *This property gets or sets an extent to restrict the map navigation.*

**Remarks**

   *N/A*

**Return Value**

[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)

---
#### `RotatedAngle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Single`

---
#### `SnapsToDevicePixels`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Style`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Style`

---
#### `TabIndex`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `Tag`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `Template`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`ControlTemplate`

---
#### `TemplatedParent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `ToolTip`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `TouchesCaptured`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IEnumerable<`TouchDevice`>

---
#### `TouchesCapturedWithin`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IEnumerable<`TouchDevice`>

---
#### `TouchesDirectlyOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IEnumerable<`TouchDevice`>

---
#### `TouchesOver`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

IEnumerable<`TouchDevice`>

---
#### `TrackOverlay`

**Summary**

   *Gets or sets the track overlay in the MapControl.*

**Remarks**

   *N/A*

**Return Value**

[`TrackInteractiveOverlay`](ThinkGeo.UI.Wpf.TrackInteractiveOverlay.md)

---
#### `Triggers`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TriggerCollection`

---
#### `Uid`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`String`

---
#### `UseLayoutRounding`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `VerticalAlignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`VerticalAlignment`

---
#### `VerticalContentAlignment`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`VerticalAlignment`

---
#### `Visibility`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Visibility`

---
#### `Width`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `ZoomLevelSet`

**Summary**

   *This property gets or sets the ZoomLevelSet used for the WpfMap control.*

**Remarks**

   *N/A*

**Return Value**

[`ZoomLevelSet`](../ThinkGeo.Core/ThinkGeo.Core.ZoomLevelSet.md)

---

### Protected Properties

#### `AncestorChangeInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Animatable_IsResourceInvalidationNecessary`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `AreTransformsClean`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ArrangeDirty`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ArrangeInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `BitmapEffectEmulationDisabled`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `BypassLayoutPolicies`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CacheModeChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `CanBeInheritanceContext`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ClipChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `ClipToBoundsCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `CommandBindingsInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CommandBindingCollection`

---
#### `ContentsChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `DefaultStyleKey`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object`

---
#### `DTypeThemeStyleKey`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObjectType`

---
#### `EffectChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `EffectiveValues`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EffectiveValueEntry[]`

---
#### `EffectiveValuesCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`UInt32`

---
#### `EffectiveValuesInitialSize`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `EventCanvas`

**Summary**

   *Gets a canvas control to hook all the events of the map.*

**Remarks**

   *N/A*

**Return Value**

`Canvas`

---
#### `EventHandlersStore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandlersStore`

---
#### `Freezable_Frozen`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Freezable_HasMultipleInheritanceContexts`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Freezable_UsingContextList`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Freezable_UsingHandlerList`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Freezable_UsingSingletonContext`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `Freezable_UsingSingletonHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `GuidelinesChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `HandlesScrolling`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasAutomationPeer`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasEffectiveKeyboardFocus`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasFefLoadedChangeHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasImplicitStyleFromResources`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasLocalStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasLogicalChildren`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasMultipleInheritanceContexts`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasNumberSubstitutionChanged`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasResourceReference`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasResources`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasStyleChanged`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasStyleEverBeenFetched`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasStyleInvalidated`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasTemplateChanged`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasTemplateGeneratedSubTree`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasThemeStyleEverBeenFetched`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `HasVisualChildren`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IAnimatable_HasAnimatedProperties`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `InheritableEffectiveValuesCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`UInt32`

---
#### `InheritableProperties`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

FrugalObjectList<`DependencyProperty`>

---
#### `InheritanceBehavior`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`InheritanceBehavior`

---
#### `InheritanceContext`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `InheritanceParent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `InputBindingsInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`InputBindingCollection`

---
#### `InternalVisual2DOr3DChildrenCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `InternalVisualChildrenCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `InternalVisualParent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `InVisibilityCollapsedTree`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsEnabledCore`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsInheritanceContextSealed`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsLoadedCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsLogicalChildrenIterationInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsParentAnFE`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsRequestingExpression`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsRightToLeft`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsRootElement`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsSelfInheritanceParent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStyleSetFromGenerator`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsStyleUpdateInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsTemplatedParentAnFE`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsTemplateRoot`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsThemeStyleUpdateInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `IsVisualChildrenIterationInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `LoadedPending`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object[]`

---
#### `LogicalChildren`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`IEnumerator`

---
#### `MeasureDirty`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `MeasureDuringArrange`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `MeasureInProgress`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `NeverArranged`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `NeverMeasured`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `OpacityMaskChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `OverlayCanvas`

**Summary**

   *Gets a canvas control to maintain current overlays in the DOM tree.*

**Remarks**

   *N/A*

**Return Value**

`Canvas`

---
#### `PositionAndSizeOfSetController`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`UIElement`

---
#### `PotentiallyHasMentees`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `PreviousArrangeRect`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Rect`

---
#### `PreviousConstraint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Size`

---
#### `ScrollableAreaClipChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `ShouldLookupImplicitStyles`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `SnapsToDevicePixelsCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `StateGroupsRoot`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FrameworkElement`

---
#### `StoresParentTemplateValues`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `StylusPlugIns`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`StylusPlugInCollection`

---
#### `SubtreeHasLoadedChangeHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `TemplateCache`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FrameworkTemplate`

---
#### `TemplateChild`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`UIElement`

---
#### `TemplateChildIndex`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `TemplateInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`FrameworkTemplate`

---
#### `ThemeStyle`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Style`

---
#### `ThisHasLoadedChangeEventHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `ToolsGrid`

**Summary**

   *Gets a grid control to hold all the map tools.*

**Remarks**

   *N/A*

**Return Value**

`Grid`

---
#### `TransformChangedHandler`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EventHandler`

---
#### `TreeLevel`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`UInt32`

---
#### `UnloadedPending`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Object[]`

---
#### `VisualBitmapEffect`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffect`

---
#### `VisualBitmapEffectInput`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffectInput`

---
#### `VisualBitmapEffectInputInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffectInput`

---
#### `VisualBitmapEffectInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapEffect`

---
#### `VisualBitmapScalingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`BitmapScalingMode`

---
#### `VisualCacheMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`CacheMode`

---
#### `VisualChildrenCount`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Int32`

---
#### `VisualClearTypeHint`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`ClearTypeHint`

---
#### `VisualClip`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Geometry`

---
#### `VisualContentBounds`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Rect`

---
#### `VisualDescendantBounds`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Rect`

---
#### `VisualEdgeMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`EdgeMode`

---
#### `VisualEffect`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Effect`

---
#### `VisualEffectInternal`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Effect`

---
#### `VisualOffset`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Vector`

---
#### `VisualOpacity`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Double`

---
#### `VisualOpacityMask`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Brush`

---
#### `VisualParent`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DependencyObject`

---
#### `VisualScrollableAreaClip`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

Nullable<`Rect`>

---
#### `VisualStateChangeSuspended`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Boolean`

---
#### `VisualTextHintingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TextHintingMode`

---
#### `VisualTextRenderingMode`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`TextRenderingMode`

---
#### `VisualTransform`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`Transform`

---
#### `VisualXSnappingGuidelines`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DoubleCollection`

---
#### `VisualYSnappingGuidelines`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

`DoubleCollection`

---

### Public Methods

#### `AddHandler(RoutedEvent,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|routedEvent|`RoutedEvent`|N/A|
|handler|`Delegate`|N/A|

---
#### `AddHandler(RoutedEvent,Delegate,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|routedEvent|`RoutedEvent`|N/A|
|handler|`Delegate`|N/A|
|handledEventsToo|`Boolean`|N/A|

---
#### `AddToEventRoute(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|e|`RoutedEventArgs`|N/A|

---
#### `ApplyAnimationClock(DependencyProperty,AnimationClock)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|clock|`AnimationClock`|N/A|

---
#### `ApplyAnimationClock(DependencyProperty,AnimationClock,HandoffBehavior)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|clock|`AnimationClock`|N/A|
|handoffBehavior|`HandoffBehavior`|N/A|

---
#### `ApplyTemplate()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Arrange(Rect)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|finalRect|`Rect`|N/A|

---
#### `BeginAnimation(DependencyProperty,AnimationTimeline)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|animation|`AnimationTimeline`|N/A|

---
#### `BeginAnimation(DependencyProperty,AnimationTimeline,HandoffBehavior)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|animation|`AnimationTimeline`|N/A|
|handoffBehavior|`HandoffBehavior`|N/A|

---
#### `BeginInit()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `BeginStoryboard(Storyboard)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|storyboard|`Storyboard`|N/A|

---
#### `BeginStoryboard(Storyboard,HandoffBehavior)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|storyboard|`Storyboard`|N/A|
|handoffBehavior|`HandoffBehavior`|N/A|

---
#### `BeginStoryboard(Storyboard,HandoffBehavior,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|storyboard|`Storyboard`|N/A|
|handoffBehavior|`HandoffBehavior`|N/A|
|isControllable|`Boolean`|N/A|

---
#### `BringIntoView()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `BringIntoView(Rect)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetRectangle|`Rect`|N/A|

---
#### `CaptureMouse()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CaptureStylus()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CaptureTouch(TouchDevice)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|touchDevice|`TouchDevice`|N/A|

---
#### `CenterAt(PointShape)`

**Summary**

   *Locates the map center to the specified world point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A world point to locate the map.|

---
#### `CenterAt(ScreenPointF)`

**Summary**

   *Locates the map center to the specified screen point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|A screen point to locate the map.|

---
#### `CenterAt(Feature)`

**Summary**

   *Locates the map center to the center of the specified feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|A feature to locates the map center.|

---
#### `CenterAt(Single,Single)`

**Summary**

   *Locates the map center to the center of the specified feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenX|`Single`|Screen x to locate the map center.|
|screenY|`Single`|Screen y to locate the map center.|

---
#### `CheckAccess()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ClearValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `ClearValue(DependencyPropertyKey)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`DependencyPropertyKey`|N/A|

---
#### `CoerceValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `Dispose()`

**Summary**

   *Disposes unmanaged objects in map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `EndInit()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Equals(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|obj|`Object`|N/A|

---
#### `FindCommonVisualAncestor(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|otherVisual|`DependencyObject`|N/A|

---
#### `FindFeatureLayer(String)`

**Summary**

   *Finds a FeatureLayer by the specified key.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`FeatureLayer`](../ThinkGeo.Core/ThinkGeo.Core.FeatureLayer.md)|A FeatureLayer which is paired with the specified key in the map overlays.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|A key of a FeatureLayer which is defined when adding the layer into the LayerOverlay.|

---
#### `FindName(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|N/A|

---
#### `FindRasterLayer(String)`

**Summary**

   *Finds a RasterLayer by the specified key.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`RasterLayer`](../ThinkGeo.Core/ThinkGeo.Core.RasterLayer.md)|A RasterLayer which is paired with the specified key in the map overlays.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`String`|A key of a RasterLayer which is defined when adding the layer into the LayerOverlay.|

---
#### `FindResource(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|resourceKey|`Object`|N/A|

---
#### `Focus()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetAnimationBaseValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `GetBindingExpression(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`BindingExpression`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `GetHashCode()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetLocalValueEnumerator()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`LocalValueEnumerator`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetSnappedExtent(RectangleShape)`

**Summary**

   *Gets a snapped extent from a specified extent.*

**Remarks**

   *The specified extent may not be the exact extent to be displayed in the pre-defined zoom level list. To display properly, the source extent needs to be snapped first.*

**Return Value**

|Type|Description|
|---|---|
|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|An extent that is snapped from the provided extent.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|An extent to be snapped.|

---
#### `GetSnappedZoomLevelIndex(RectangleShape)`

**Summary**

   *Gets the snapped zoom level index from the provided world extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|A snapped zoom level index in the zoom levels sychnonized by the SyncZoomLevelScales method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|A world extent to be snapped.|

---
#### `GetSnappedZoomLevelIndex(Double)`

**Summary**

   *Gets the snapped zoom level index from the provided scale.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Int32`|A snapped zoom level index in scale list sychnonized by the SyncZoomLevelScales method.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|scale|`Double`|A scale to be snapped.|

---
#### `GetType()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Type`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `GetVersion()`

**Summary**

   *This static method will be useful when you want to report a bug in a specified version of Map Suite. You can use it to tell ThinkGeo support which version you are trying to use.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|A string representing the version of the MapSuiteCore and Map Suite Desktop product that you are now using.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InputHitTest(Point)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`IInputElement`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|

---
#### `InvalidateArrange()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvalidateMeasure()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvalidateProperty(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `InvalidateVisual()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `IsAncestorOf(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|descendant|`DependencyObject`|N/A|

---
#### `IsDescendantOf(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ancestor|`DependencyObject`|N/A|

---
#### `LoadState(Byte[])`

**Summary**

   *This method restore map state back from the specified state.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|state|`Byte[]`|This parameter indicates the state for restore the map.|

---
#### `Measure(Size)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|availableSize|`Size`|N/A|

---
#### `MoveFocus(TraversalRequest)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|request|`TraversalRequest`|N/A|

---
#### `OnApplyTemplate()`

**Summary**

   *Applys current map template.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Pan(Single,Int32)`

**Summary**

   *This function will pan the currentExtent based on an angle and percentage.*

**Remarks**

   *This method will change the current extent by panning according to the angle and percentage specified.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|degree|`Single`|This parameter is the degree (angle) in which you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(PanDirection,Int32)`

**Summary**

   *This function will pan the currentExtent based on a direction and percentage.*

**Remarks**

   *This method will change the current extent by panning according to the direction and percentage specified.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|None.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|panDirection|[`PanDirection`](../ThinkGeo.Core/ThinkGeo.Core.PanDirection.md)|This parameter is the direction in which you want to pan.|
|percentage|`Int32`|This parameter is the percentage by which you want to pan.|

---
#### `Pan(Double,Double)`

**Summary**

   *Pans the map by the provided screen offset.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|offsetScreenX|`Double`|Screen offset X to be paned.|
|offsetScreenY|`Double`|Screen offset Y to be paned.|

---
#### `PointFromScreen(Point)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Point`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|

---
#### `PointToScreen(Point)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Point`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|

---
#### `PredictFocus(FocusNavigationDirection)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|direction|`FocusNavigationDirection`|N/A|

---
#### `RaiseEvent(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`RoutedEventArgs`|N/A|

---
#### `ReadLocalValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `Refresh(OverlayRefreshType)`

**Summary**

   *Refreshes current map control.*

**Remarks**

   *Refreshes all the existing overlays and map tools.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|Indicates where the existing tiles will be refreshed.|

---
#### `Refresh(RectangleShape,OverlayRefreshType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `Refresh(Overlay,OverlayRefreshType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|overlay|[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)|N/A|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `Refresh(IEnumerable<Overlay>,OverlayRefreshType)`

**Summary**

   *Refreshes a specified overlay collection.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|overlays|IEnumerable<[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)>|A collection of overlay to be refreshed.|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `Refresh(RectangleShape,Overlay,OverlayRefreshType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|overlay|[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)|N/A|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `Refresh(RectangleShape,IEnumerable<Overlay>,OverlayRefreshType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|extent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|N/A|
|overlays|IEnumerable<[`Overlay`](ThinkGeo.UI.Wpf.Overlay.md)>|N/A|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|N/A|

---
#### `RegisterName(String,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|N/A|
|scopedElement|`Object`|N/A|

---
#### `ReleaseAllTouchCaptures()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ReleaseMouseCapture()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ReleaseStylusCapture()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ReleaseTouchCapture(TouchDevice)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|touchDevice|`TouchDevice`|N/A|

---
#### `RemoveHandler(RoutedEvent,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|routedEvent|`RoutedEvent`|N/A|
|handler|`Delegate`|N/A|

---
#### `SaveState()`

**Summary**

   *This method saves map state to a byte array.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|A byte array indicates current map state.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SetBinding(DependencyProperty,BindingBase)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`BindingExpressionBase`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|binding|`BindingBase`|N/A|

---
#### `SetBinding(DependencyProperty,String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`BindingExpression`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|path|`String`|N/A|

---
#### `SetCurrentValue(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Object`|N/A|

---
#### `SetResourceReference(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|name|`Object`|N/A|

---
#### `SetValue(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Object`|N/A|

---
#### `SetValue(DependencyPropertyKey,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`DependencyPropertyKey`|N/A|
|value|`Object`|N/A|

---
#### `ShouldSerializeCommandBindings()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ShouldSerializeInputBindings()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ShouldSerializeResources()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ShouldSerializeStyle()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ShouldSerializeTriggers()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToggleMapExtents()`

**Summary**

   *Toggles the map between current extent and previous extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToScreenCoordinate(Double,Double)`

**Summary**

   *Converts the provided world points to screen points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A screen coordinate that is converted.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldX|`Double`|World x to be converted.|
|worldY|`Double`|World y to be converted.|

---
#### `ToScreenCoordinate(PointShape)`

**Summary**

   *Converts the provided world points to screen points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A screen coordinate that is converted.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldCoordinate|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|World point to be converted.|

---
#### `ToScreenCoordinate(Point)`

**Summary**

   *Converts the provided world points to screen points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A screen coordinate that is converted.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|worldCoordinate|`Point`|World point to be converted.|

---
#### `ToString()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ToWorldCoordinate(Double,Double)`

**Summary**

   *Converts the provided screen points to world points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A world point that is converted from the provided parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenX|`Double`|Screen x to be converted.|
|screenY|`Double`|Screen y to be converted.|

---
#### `ToWorldCoordinate(PointShape)`

**Summary**

   *Converts the provided screen points to world points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A world point that is converted from the provided parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenCoordinate|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|Screen point to be converted.|

---
#### `ToWorldCoordinate(Point)`

**Summary**

   *Converts the provided screen points to world points.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A world point that is converted from the provided parameters.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|screenCoordinate|`Point`|Screen point to be converted.|

---
#### `TransformToAncestor(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeneralTransform`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ancestor|`Visual`|N/A|

---
#### `TransformToAncestor(Visual3D)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeneralTransform2DTo3D`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ancestor|`Visual3D`|N/A|

---
#### `TransformToDescendant(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeneralTransform`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|descendant|`Visual`|N/A|

---
#### `TransformToVisual(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeneralTransform`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|visual|`Visual`|N/A|

---
#### `TranslatePoint(Point,UIElement)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Point`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|
|relativeTo|`UIElement`|N/A|

---
#### `TryFindResource(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|resourceKey|`Object`|N/A|

---
#### `UnregisterName(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|N/A|

---
#### `UpdateDefaultStyle()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateLayout()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `VerifyAccess()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ZoomIn()`

**Summary**

   *Zooms the map in for one level*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ZoomIn(Int32)`

**Summary**

   *Zooms the map in by the provided percentage.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map in.|

---
#### `ZoomIntoCenter(Int32,Feature)`

**Summary**

   *Zooms the map in by the provided percentage and locates the map to the center of the provided feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map in.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|A new center feature of current map.|

---
#### `ZoomIntoCenter(Int32,PointShape)`

**Summary**

   *Zooms the map in by the provided percentage and locates to the center of the provided feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map in.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A new center of current map.|

---
#### `ZoomIntoCenter(Int32,ScreenPointF)`

**Summary**

   *Zooms the map in by the provided percentage and locates to the provided screen point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map in.|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|A new screen center to locate the map.|

---
#### `ZoomIntoCenter(Int32,Single,Single)`

**Summary**

   *Zooms the map in by the provided percentage and locates to the provided screen x and y.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map in.|
|screenX|`Single`|Screen x to locate the map.|
|screenY|`Single`|Screen y to locate the map.|

---
#### `ZoomOut()`

**Summary**

   *Zooms the map out for one level.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ZoomOut(Int32)`

**Summary**

   *Zooms the map out by the provided percentage.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map out.|

---
#### `ZoomOutToCenter(Int32,Feature)`

**Summary**

   *Zooms the map out by the provided percentage and locates map to the center of the provided feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map out.|
|centerFeature|[`Feature`](../ThinkGeo.Core/ThinkGeo.Core.Feature.md)|A new center feature of current map.|

---
#### `ZoomOutToCenter(Int32,PointShape)`

**Summary**

   *Zooms the map out by the provided percentage and locates to the center of the provided feature.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map out.|
|worldPoint|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A new center of current map.|

---
#### `ZoomOutToCenter(Int32,ScreenPointF)`

**Summary**

   *Zooms the map out by the provided percentage and locates to the provided screen point.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map out.|
|screenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|A new screen center to locate the map.|

---
#### `ZoomOutToCenter(Int32,Single,Single)`

**Summary**

   *Zooms the map out by the provided percentage and locates to the provided screen x and y.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|percentage|`Int32`|A scale to zoom the map out.|
|screenX|`Single`|Screen x to locate the map.|
|screenY|`Single`|Screen y to locate the map.|

---
#### `ZoomTo(PointShape,Double)`

**Summary**

   *This method zooms current map to a specified position and scale.*

**Remarks**

   *When calling this method, it doesn't refresh existing Tiles on the current map. For example, if using a TileOverlay such as LayerOverlay; a layer style is changed, Refresh method is proper to call.*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetWorldCenter|[`PointShape`](../ThinkGeo.Core/ThinkGeo.Core.PointShape.md)|A world center to zoom the map to.|
|targetScale|`Double`|A double value indicates the scale to zoom the map to.|

---
#### `ZoomToPreviousExtent()`

**Summary**

   *Zooms the map to the previous extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ZoomToScale(Double)`

**Summary**

   *Zooms the map to a provided scale.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|Target scale to zoom the map.|

---
#### `ZoomToScale(Double,ScreenPointF)`

**Summary**

   *Zooms the map to a provided scale and locates the map by the provided screen offset.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|Target scale to zoom the map.|
|offsetScreenPoint|[`ScreenPointF`](../ThinkGeo.Core/ThinkGeo.Core.ScreenPointF.md)|An screen offset to locate the map.|

---
#### `ZoomToScale(Double,Single,Single)`

**Summary**

   *Zooms the map to a provided scale and locates the map by the provided screen offset.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetScale|`Double`|Target scale to zoom the map.|
|offsetScreenX|`Single`|An screen offset x to locate the map.|
|offsetScreenY|`Single`|An screen offset y to locate the map.|

---

### Protected Methods

#### `AddInheritanceContext(DependencyObject,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|context|`DependencyObject`|N/A|
|property|`DependencyProperty`|N/A|

---
#### `AddLogicalChild(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Object`|N/A|

---
#### `AddRefOnChannelCore(Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`ResourceHandle`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|

---
#### `AddRefOnChannelForCyclicBrush(ICyclicBrush,Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|cyclicBrush|`ICyclicBrush`|N/A|
|channel|`Channel`|N/A|

---
#### `AddSynchronizedInputPostOpportunityHandler(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `AddSynchronizedInputPreOpportunityHandler(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `AddSynchronizedInputPreOpportunityHandlerCore(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `AddToEventRouteCore(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `AddVisualChild(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Visual`|N/A|

---
#### `AdjustBranchSource(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `AdjustEventSource(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `ArrangeCore(Rect)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|finalRect|`Rect`|N/A|

---
#### `ArrangeOverride(Size)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Size`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|arrangeBounds|`Size`|N/A|

---
#### `BeginPropertyInitialization()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `BitmapEffectEmulationChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `BlockReverseInheritance()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `BuildRoute(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `BuildRouteCore(EventRoute,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `BuildRouteCoreHelper(EventRoute,RoutedEventArgs,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|route|`EventRoute`|N/A|
|args|`RoutedEventArgs`|N/A|
|shouldAddIntermediateElementsToRoute|`Boolean`|N/A|

---
#### `CacheModeChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `CalculateSubgraphBoundsInnerSpace()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CalculateSubgraphBoundsInnerSpace(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|renderBounds|`Boolean`|N/A|

---
#### `CalculateSubgraphBoundsOuterSpace()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CalculateSubgraphRenderBoundsInnerSpace()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CalculateSubgraphRenderBoundsOuterSpace()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CancelSynchronizedInput()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ChangeLogicalParent(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|newParent|`DependencyObject`|N/A|

---
#### `ChangeSubtreeHasLoadedChangedHandler(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|mentor|`DependencyObject`|N/A|

---
#### `ChangeValidationVisualState(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|useTransitions|`Boolean`|N/A|

---
#### `ChangeVisualClip(Geometry,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|newClip|`Geometry`|N/A|
|dontSetWhenClose|`Boolean`|N/A|

---
#### `ChangeVisualState(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|useTransitions|`Boolean`|N/A|

---
#### `CheckFlagsAnd(Channel,VisualProxyFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|
|flagsToCheck|`VisualProxyFlags`|N/A|

---
#### `CheckFlagsAnd(VisualFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|flags|`VisualFlags`|N/A|

---
#### `CheckFlagsOnAllChannels(VisualProxyFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|flagsToCheck|`VisualProxyFlags`|N/A|

---
#### `CheckFlagsOr(Channel,VisualProxyFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|
|flagsToCheck|`VisualProxyFlags`|N/A|

---
#### `CheckFlagsOr(VisualFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|flags|`VisualFlags`|N/A|

---
#### `ClipChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `ContainsValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `ContentsChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `ContextVerifiedGetParent()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CreateAutomationPeer()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`AutomationPeer`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `CreateGenericRootAutomationPeer()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`AutomationPeer`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Debug_AssertNoInheritanceContextListeners()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DetachFromDispatcher()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `DisconnectAttachedResource(VisualProxyFlags,IResource)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|correspondingFlag|`VisualProxyFlags`|N/A|
|attachedResource|`IResource`|N/A|

---
#### `Dispose(Boolean)`

**Summary**

   *Disposes unmanaged objects in map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|disposing|`Boolean`|N/A|

---
#### `Draw(RectangleShape,OverlayRefreshType)`

**Summary**

   *Draws the map by the provided extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|An extent that to draw current map.|
|refreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|Indicates whether the map needs to be refreshed. For example, a style of layer is changed, the overlay needs to be refreshed. When panning, the overlay only changes its position, refresh is not needed.|

---
#### `DrawCore(RectangleShape,OverlayRefreshType)`

**Summary**

   *Draws the map by the provided extent.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetExtent|[`RectangleShape`](../ThinkGeo.Core/ThinkGeo.Core.RectangleShape.md)|An extent that to draw current map.|
|overlayRefreshType|[`OverlayRefreshType`](ThinkGeo.UI.Wpf.OverlayRefreshType.md)|Indicates whether the map needs to be refreshed. For example, a style of layer is changed, the overlay needs to be refreshed. When panning, the overlay only changes its position, refresh is not needed.|

---
#### `EffectChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `EndPropertyInitialization()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `EnsureEventHandlersStore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Enter()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `EvaluateAnimatedValueCore(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|entry|`EffectiveValueEntry&`|N/A|

---
#### `EvaluateBaseValueCore(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|newEntry|`EffectiveValueEntry&`|N/A|

---
#### `EventHandlersStoreAdd(EventPrivateKey,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`EventPrivateKey`|N/A|
|handler|`Delegate`|N/A|

---
#### `EventHandlersStoreRemove(EventPrivateKey,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`EventPrivateKey`|N/A|
|handler|`Delegate`|N/A|

---
#### `Exit()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `Finalize()`

**Summary**

   *Finalize method of WpfMap class.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `FindFirstAncestorWithFlagsAnd(VisualFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|flag|`VisualFlags`|N/A|

---
#### `FindName(String,DependencyObject&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|name|`String`|N/A|
|scopeOwner|`DependencyObject&`|N/A|

---
#### `FindResourceOnSelf(Object,Boolean,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|resourceKey|`Object`|N/A|
|allowDeferredResourceReference|`Boolean`|N/A|
|mustReturnDeferredResourceReference|`Boolean`|N/A|

---
#### `FireLoadedOnDescendentsInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `FireOnVisualParentChanged(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldParent|`DependencyObject`|N/A|

---
#### `FireUnloadedOnDescendentsInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `FreeContent(Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|

---
#### `GetAutomationPeer()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`AutomationPeer`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetContentBounds()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDpi()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DpiScale`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetDrawing()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DrawingGroup`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetExpressionCore(DependencyProperty,PropertyMetadata)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Expression`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|

---
#### `GetHitTestBounds()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Rect`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetLayoutClip(Size)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Geometry`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|layoutSlotSize|`Size`|N/A|

---
#### `GetLayoutClipInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Geometry`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetPlainText()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`String`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetRawValue(DependencyProperty,PropertyMetadata,EffectiveValueEntry&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|entry|`EffectiveValueEntry&`|N/A|

---
#### `GetTemplateChild(String)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|childName|`String`|N/A|

---
#### `GetUIParent()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetUIParent(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|continuePastVisualTree|`Boolean`|N/A|

---
#### `GetUIParentCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetUIParentNo3DTraversal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetUIParentOrICH(UIElement&,IContentHost&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|uiParent|`UIElement&`|N/A|
|ich|`IContentHost&`|N/A|

---
#### `GetUIParentWithinLayoutIsland()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`UIElement`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `GetValueEntry(EntryIndex,DependencyProperty,PropertyMetadata,RequestFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`EffectiveValueEntry`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|requests|`RequestFlags`|N/A|

---
#### `GetValueSource(DependencyProperty,PropertyMetadata,Boolean&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`BaseValueSourceInternal`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|hasModifiers|`Boolean&`|N/A|

---
#### `GetValueSource(DependencyProperty,PropertyMetadata,Boolean&,Boolean&,Boolean&,Boolean&,Boolean&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`BaseValueSourceInternal`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|hasModifiers|`Boolean&`|N/A|
|isExpression|`Boolean&`|N/A|
|isAnimated|`Boolean&`|N/A|
|isCoerced|`Boolean&`|N/A|
|isCurrent|`Boolean&`|N/A|

---
#### `GetVisualChild(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Visual`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|N/A|

---
#### `GetZoomLevelScale(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Double`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|N/A|

---
#### `GuidelinesChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `HasAnyExpression()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `HasExpression(EntryIndex,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|

---
#### `HasNonDefaultValue(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `HitTest(Point)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResult`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|

---
#### `HitTest(Point,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResult`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|point|`Point`|N/A|
|include2DOn3D|`Boolean`|N/A|

---
#### `HitTest(HitTestFilterCallback,HitTestResultCallback,HitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|filterCallback|`HitTestFilterCallback`|N/A|
|resultCallback|`HitTestResultCallback`|N/A|
|hitTestParameters|`HitTestParameters`|N/A|

---
#### `HitTestCore(PointHitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResult`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|hitTestParameters|`PointHitTestParameters`|N/A|

---
#### `HitTestCore(GeometryHitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeometryHitTestResult`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|hitTestParameters|`GeometryHitTestParameters`|N/A|

---
#### `HitTestGeometry(HitTestFilterCallback,HitTestResultCallback,GeometryHitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResultBehavior`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|filterCallback|`HitTestFilterCallback`|N/A|
|resultCallback|`HitTestResultCallback`|N/A|
|geometryParams|`GeometryHitTestParameters`|N/A|

---
#### `HitTestPoint(HitTestFilterCallback,HitTestResultCallback,PointHitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResultBehavior`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|filterCallback|`HitTestFilterCallback`|N/A|
|resultCallback|`HitTestResultCallback`|N/A|
|pointParams|`PointHitTestParameters`|N/A|

---
#### `HitTestPointInternal(HitTestFilterCallback,HitTestResultCallback,PointHitTestParameters)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`HitTestResultBehavior`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|filterCallback|`HitTestFilterCallback`|N/A|
|resultCallback|`HitTestResultCallback`|N/A|
|hitTestParameters|`PointHitTestParameters`|N/A|

---
#### `IgnoreModelParentBuildRoute(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `InputHitTest(Point,IInputElement&,IInputElement&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pt|`Point`|N/A|
|enabledHit|`IInputElement&`|N/A|
|rawHit|`IInputElement&`|N/A|

---
#### `InputHitTest(Point,IInputElement&,IInputElement&,HitTestResult&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|pt|`Point`|N/A|
|enabledHit|`IInputElement&`|N/A|
|rawHit|`IInputElement&`|N/A|
|rawHitResult|`HitTestResult&`|N/A|

---
#### `InternalAddVisualChild(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Visual`|N/A|

---
#### `InternalGet2DOr3DVisualChild(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DependencyObject`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|N/A|

---
#### `InternalGetVisualChild(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Visual`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|index|`Int32`|N/A|

---
#### `InternalRemoveVisualChild(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Visual`|N/A|

---
#### `InternalSetOffsetWorkaround(Vector)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|offset|`Vector`|N/A|

---
#### `InternalSetTransformWorkaround(Transform)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|transform|`Transform`|N/A|

---
#### `InvalidateArrangeInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvalidateAutomationAncestorsCore(Stack<DependencyObject>,Boolean&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|branchNodeStack|Stack<`DependencyObject`>|N/A|
|continuePastCoreTree|`Boolean&`|N/A|

---
#### `InvalidateAutomationAncestorsCoreHelper(Stack<DependencyObject>,Boolean&,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|branchNodeStack|Stack<`DependencyObject`>|N/A|
|continuePastCoreTree|`Boolean&`|N/A|
|shouldInvalidateIntermediateElements|`Boolean`|N/A|

---
#### `InvalidateForceInheritPropertyOnChildren(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|property|`DependencyProperty`|N/A|

---
#### `InvalidateHitTestBounds()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvalidateMeasureInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvalidateProperty(DependencyProperty,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|preserveCurrentValue|`Boolean`|N/A|

---
#### `InvalidateSubProperty(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `InvalidateTreeDependentProperties(TreeChangeInfo,Boolean,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|FrugalObjectList<`DependencyProperty`>|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|parentTreeState|`TreeChangeInfo`|N/A|
|isSelfInheritanceParent|`Boolean`|N/A|
|wasSelfInheritanceParent|`Boolean`|N/A|

---
#### `InvalidateZOrder()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `InvokeAccessKey(AccessKeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`AccessKeyEventArgs`|N/A|

---
#### `IsOnChannel(Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|

---
#### `LoadStateCore(Byte[])`

**Summary**

   *This method restore map state back from the specified state.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|state|`Byte[]`|This parameter indicates the state for restore the map.|

---
#### `LookupEntry(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`EntryIndex`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|targetIndex|`Int32`|N/A|

---
#### `MakeSentinel()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `MeasureCore(Size)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Size`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|availableSize|`Size`|N/A|

---
#### `MeasureOverride(Size)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Size`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|constraint|`Size`|N/A|

---
#### `MemberwiseClone()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `NotifyPropertyChange(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `NotifySubPropertyChange(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `OnAccessKey(AccessKeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`AccessKeyEventArgs`|N/A|

---
#### `OnAddHandler(RoutedEvent,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|routedEvent|`RoutedEvent`|N/A|
|handler|`Delegate`|N/A|

---
#### `OnAncestorChanged()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnAncestorChangedInternal(TreeChangeInfo)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|parentTreeState|`TreeChangeInfo`|N/A|

---
#### `OnChildDesiredSizeChanged(UIElement)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`UIElement`|N/A|

---
#### `OnContextMenuClosing(ContextMenuEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ContextMenuEventArgs`|N/A|

---
#### `OnContextMenuOpening(ContextMenuEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ContextMenuEventArgs`|N/A|

---
#### `OnCreateAutomationPeer()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`AutomationPeer`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnCreateAutomationPeerInternal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`AutomationPeer`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnCurrentExtentChanged(CurrentExtentChangedMapViewEventArgs)`

**Summary**

   *Occurs when map's current extent is changed.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CurrentExtentChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangedMapViewEventArgs.md)|Event argument for CurrentExtentChanged event.|

---
#### `OnCurrentExtentChanging(CurrentExtentChangingMapViewEventArgs)`

**Summary**

   *Occurs when map's current extent is changing.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CurrentExtentChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangingMapViewEventArgs.md)|Event argument for CurrentExtentChanging event.|

---
#### `OnCurrentScaleChanged(CurrentScaleChangedMapViewEventArgs)`

**Summary**

   *Occurs when map's current scale is changed.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CurrentScaleChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangedMapViewEventArgs.md)|Event argument for CurrentScaleChanged event.|

---
#### `OnCurrentScaleChanging(CurrentScaleChangingMapViewEventArgs)`

**Summary**

   *Occurs when map's current scale is changing.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`CurrentScaleChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangingMapViewEventArgs.md)|Event argument for CurrentScaleChanging event.|

---
#### `OnDpiChanged(DpiScale,DpiScale)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldDpi|`DpiScale`|N/A|
|newDpi|`DpiScale`|N/A|

---
#### `OnDragEnter(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnDragLeave(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnDragOver(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnDrop(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnGiveFeedback(GiveFeedbackEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`GiveFeedbackEventArgs`|N/A|

---
#### `OnGotFocus(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`RoutedEventArgs`|N/A|

---
#### `OnGotKeyboardFocus(KeyboardFocusChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyboardFocusChangedEventArgs`|N/A|

---
#### `OnGotMouseCapture(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnGotStylusCapture(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnGotTouchCapture(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnInheritanceContextChanged(EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`EventArgs`|N/A|

---
#### `OnInheritanceContextChangedCore(EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`EventArgs`|N/A|

---
#### `OnInitialized(EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`EventArgs`|N/A|

---
#### `OnIsKeyboardFocusedChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsMouseCapturedChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsMouseDirectlyOverChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsStylusCapturedChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnIsStylusDirectlyOverChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnKeyDown(KeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyEventArgs`|N/A|

---
#### `OnKeyUp(KeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyEventArgs`|N/A|

---
#### `OnLoaded(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `OnLostFocus(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`RoutedEventArgs`|N/A|

---
#### `OnLostKeyboardFocus(KeyboardFocusChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyboardFocusChangedEventArgs`|N/A|

---
#### `OnLostMouseCapture(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnLostStylusCapture(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnLostTouchCapture(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationBoundaryFeedbackEventArgs`|N/A|

---
#### `OnManipulationCompleted(ManipulationCompletedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationCompletedEventArgs`|N/A|

---
#### `OnManipulationDelta(ManipulationDeltaEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationDeltaEventArgs`|N/A|

---
#### `OnManipulationInertiaStarting(ManipulationInertiaStartingEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationInertiaStartingEventArgs`|N/A|

---
#### `OnManipulationStarted(ManipulationStartedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationStartedEventArgs`|N/A|

---
#### `OnManipulationStarting(ManipulationStartingEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ManipulationStartingEventArgs`|N/A|

---
#### `OnMapClick(MapClickMapViewEventArgs)`

**Summary**

   *Occurs when clicking on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)|Event argument for MapClick event.|

---
#### `OnMapDoubleClick(MapClickMapViewEventArgs)`

**Summary**

   *Occurs when double clicking on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)|Event argument for MapDoubleClick event.|

---
#### `OnMapTap(MapTapMapViewEventArgs)`

**Summary**

   *Occurs when tapping on the map.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`MapTapMapViewEventArgs`](ThinkGeo.UI.Wpf.MapTapMapViewEventArgs.md)|Event argument for MapTap event.|

---
#### `OnMouseDoubleClick(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseEnter(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnMouseLeave(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnMouseLeftButtonDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseLeftButtonUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseMove(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnMouseRightButtonDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseRightButtonUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnMouseWheel(MouseWheelEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseWheelEventArgs`|N/A|

---
#### `OnNewParent(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|newParent|`DependencyObject`|N/A|

---
#### `OnOverlayDrawing(OverlayDrawingMapViewEventArgs)`

**Summary**

   *Occurs when an overlay is drawing.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OverlayDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawingMapViewEventArgs.md)|Event argument for OverlayDrawing event.|

---
#### `OnOverlayDrawn(OverlayDrawnMapViewEventArgs)`

**Summary**

   *Occurs when an overlay is drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OverlayDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawnMapViewEventArgs.md)|Event argument for OverlayDrawn event.|

---
#### `OnOverlaysDrawing(OverlaysDrawingMapViewEventArgs)`

**Summary**

   *Occurs when all overlays are drawing.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OverlaysDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawingMapViewEventArgs.md)|Event argument for OverlaysDrawing event.|

---
#### `OnOverlaysDrawn(OverlaysDrawnMapViewEventArgs)`

**Summary**

   *Occurs when overlays are drawn.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`OverlaysDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawnMapViewEventArgs.md)|Event argument for OverlaysDrawn event.|

---
#### `OnPostApplyTemplate()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnPreApplyTemplate()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnPresentationSourceChanged(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|attached|`Boolean`|N/A|

---
#### `OnPreviewDragEnter(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnPreviewDragLeave(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnPreviewDragOver(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnPreviewDrop(DragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DragEventArgs`|N/A|

---
#### `OnPreviewGiveFeedback(GiveFeedbackEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`GiveFeedbackEventArgs`|N/A|

---
#### `OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyboardFocusChangedEventArgs`|N/A|

---
#### `OnPreviewKeyDown(KeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyEventArgs`|N/A|

---
#### `OnPreviewKeyUp(KeyEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyEventArgs`|N/A|

---
#### `OnPreviewLostKeyboardFocus(KeyboardFocusChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`KeyboardFocusChangedEventArgs`|N/A|

---
#### `OnPreviewMouseDoubleClick(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseLeftButtonDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseLeftButtonUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseMove(MouseEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseEventArgs`|N/A|

---
#### `OnPreviewMouseRightButtonDown(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseRightButtonUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseUp(MouseButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseButtonEventArgs`|N/A|

---
#### `OnPreviewMouseWheel(MouseWheelEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`MouseWheelEventArgs`|N/A|

---
#### `OnPreviewQueryContinueDrag(QueryContinueDragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`QueryContinueDragEventArgs`|N/A|

---
#### `OnPreviewStylusButtonDown(StylusButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusButtonEventArgs`|N/A|

---
#### `OnPreviewStylusButtonUp(StylusButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusButtonEventArgs`|N/A|

---
#### `OnPreviewStylusDown(StylusDownEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusDownEventArgs`|N/A|

---
#### `OnPreviewStylusInAirMove(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnPreviewStylusInRange(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnPreviewStylusMove(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnPreviewStylusOutOfRange(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnPreviewStylusSystemGesture(StylusSystemGestureEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusSystemGestureEventArgs`|N/A|

---
#### `OnPreviewStylusUp(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnPreviewTextInput(TextCompositionEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TextCompositionEventArgs`|N/A|

---
#### `OnPreviewTouchDown(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnPreviewTouchMove(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnPreviewTouchUp(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnPropertyChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `OnQueryContinueDrag(QueryContinueDragEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`QueryContinueDragEventArgs`|N/A|

---
#### `OnQueryCursor(QueryCursorEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`QueryCursorEventArgs`|N/A|

---
#### `OnRemoveHandler(RoutedEvent,Delegate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|routedEvent|`RoutedEvent`|N/A|
|handler|`Delegate`|N/A|

---
#### `OnRender(DrawingContext)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|drawingContext|`DrawingContext`|N/A|

---
#### `OnRenderSizeChanged(SizeChangedInfo)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sizeInfo|`SizeChangedInfo`|N/A|

---
#### `OnStyleChanged(Style,Style)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldStyle|`Style`|N/A|
|newStyle|`Style`|N/A|

---
#### `OnStylusButtonDown(StylusButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusButtonEventArgs`|N/A|

---
#### `OnStylusButtonUp(StylusButtonEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusButtonEventArgs`|N/A|

---
#### `OnStylusDown(StylusDownEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusDownEventArgs`|N/A|

---
#### `OnStylusEnter(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusInAirMove(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusInRange(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusLeave(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusMove(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusOutOfRange(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnStylusSystemGesture(StylusSystemGestureEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusSystemGestureEventArgs`|N/A|

---
#### `OnStylusUp(StylusEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`StylusEventArgs`|N/A|

---
#### `OnTemplateChanged(ControlTemplate,ControlTemplate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldTemplate|`ControlTemplate`|N/A|
|newTemplate|`ControlTemplate`|N/A|

---
#### `OnTemplateChangedInternal(FrameworkTemplate,FrameworkTemplate)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldTemplate|`FrameworkTemplate`|N/A|
|newTemplate|`FrameworkTemplate`|N/A|

---
#### `OnTextInput(TextCompositionEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TextCompositionEventArgs`|N/A|

---
#### `OnThemeChanged()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `OnToolTipClosing(ToolTipEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ToolTipEventArgs`|N/A|

---
#### `OnToolTipOpening(ToolTipEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`ToolTipEventArgs`|N/A|

---
#### `OnTouchDown(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnTouchEnter(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnTouchLeave(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnTouchMove(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnTouchUp(TouchEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|`TouchEventArgs`|N/A|

---
#### `OnUnloaded(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `OnVisualAncestorChanged(Object,AncestorChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`AncestorChangedEventArgs`|N/A|

---
#### `OnVisualAncestorChanged(Object,AncestorChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`AncestorChangedEventArgs`|N/A|

---
#### `OnVisualChildrenChanged(DependencyObject,DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|visualAdded|`DependencyObject`|N/A|
|visualRemoved|`DependencyObject`|N/A|

---
#### `OnVisualParentChanged(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldParent|`DependencyObject`|N/A|

---
#### `OnZoomLevelSetChanged(ZoomLevelSetChangedMapViewEventArgs)`

**Summary**

   *Occurs when map's zoom level set is changed.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|e|[`ZoomLevelSetChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.ZoomLevelSetChangedMapViewEventArgs.md)|Event argument for ZoomLevelSetChanged event.|

---
#### `OpacityMaskChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `ParentLayoutInvalidated(UIElement)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`UIElement`|N/A|

---
#### `Precompute()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `PrecomputeContent()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `PrecomputeRecursive(Rect&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|bboxSubgraph|`Rect&`|N/A|

---
#### `PropagateChangedFlags()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ProvideSelfAsInheritanceContext(Object,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Object`|N/A|
|dp|`DependencyProperty`|N/A|

---
#### `ProvideSelfAsInheritanceContext(DependencyObject,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|doValue|`DependencyObject`|N/A|
|dp|`DependencyProperty`|N/A|

---
#### `pushTextRenderingMode()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `RaiseClrEvent(EventPrivateKey,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|key|`EventPrivateKey`|N/A|
|args|`EventArgs`|N/A|

---
#### `RaiseEvent(RoutedEventArgs,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|
|trusted|`Boolean`|N/A|

---
#### `RaiseInheritedPropertyChangedEvent(InheritablePropertyChangeInfo&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|info|`InheritablePropertyChangeInfo&`|N/A|

---
#### `RaiseIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `RaiseIsMouseCaptureWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `RaiseIsStylusCaptureWithinChanged(DependencyPropertyChangedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DependencyPropertyChangedEventArgs`|N/A|

---
#### `RaiseTrustedEvent(RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`RoutedEventArgs`|N/A|

---
#### `ReadControlFlag(ControlBoolFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`ControlBoolFlags`|N/A|

---
#### `ReadFlag(CoreFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|field|`CoreFlags`|N/A|

---
#### `ReadInternalFlag(InternalFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`InternalFlags`|N/A|

---
#### `ReadInternalFlag2(InternalFlags2)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`InternalFlags2`|N/A|

---
#### `ReadLocalValueEntry(EntryIndex,DependencyProperty,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Object`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|allowDeferredReferences|`Boolean`|N/A|

---
#### `RecursiveSetDpiScaleVisualFlags(DpiRecursiveChangeArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DpiRecursiveChangeArgs`|N/A|

---
#### `ReleaseOnChannelCore(Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|

---
#### `ReleaseOnChannelForCyclicBrush(ICyclicBrush,Channel)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|cyclicBrush|`ICyclicBrush`|N/A|
|channel|`Channel`|N/A|

---
#### `RemoveInheritanceContext(DependencyObject,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|context|`DependencyObject`|N/A|
|property|`DependencyProperty`|N/A|

---
#### `RemoveLogicalChild(Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Object`|N/A|

---
#### `RemoveSelfAsInheritanceContext(Object,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Object`|N/A|
|dp|`DependencyProperty`|N/A|

---
#### `RemoveSelfAsInheritanceContext(DependencyObject,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|doValue|`DependencyObject`|N/A|
|dp|`DependencyProperty`|N/A|

---
#### `RemoveVisualChild(Visual)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|child|`Visual`|N/A|

---
#### `Render(RenderContext,UInt32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ctx|`RenderContext`|N/A|
|childIndex|`UInt32`|N/A|

---
#### `RenderClose(IDrawingContent)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|newContent|`IDrawingContent`|N/A|

---
#### `RenderContent(RenderContext,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ctx|`RenderContext`|N/A|
|isOnChannel|`Boolean`|N/A|

---
#### `RenderOpen()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`DrawingContext`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `RenderRecursive(RenderContext)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ctx|`RenderContext`|N/A|

---
#### `SaveStateCore()`

**Summary**

   *This method saves map state to a byte array.*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Byte[]`|A byte array indicates current map state.|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `ScrollableAreaClipChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `Seal()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SetCurrentDeferredValue(DependencyProperty,DeferredReference)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|deferredReference|`DeferredReference`|N/A|

---
#### `SetCurrentValue(DependencyProperty,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Boolean`|N/A|

---
#### `SetCurrentValueInternal(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Object`|N/A|

---
#### `SetDeferredValue(DependencyProperty,DeferredReference)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|deferredReference|`DeferredReference`|N/A|

---
#### `SetDpiScaleVisualFlags(DpiRecursiveChangeArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|args|`DpiRecursiveChangeArgs`|N/A|

---
#### `SetEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata,EffectiveValueEntry,EffectiveValueEntry)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|newEntry|`EffectiveValueEntry`|N/A|
|oldEntry|`EffectiveValueEntry`|N/A|

---
#### `SetEffectiveValue(EntryIndex,DependencyProperty,Int32,PropertyMetadata,Object,BaseValueSourceInternal)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|targetIndex|`Int32`|N/A|
|metadata|`PropertyMetadata`|N/A|
|value|`Object`|N/A|
|valueSource|`BaseValueSourceInternal`|N/A|

---
#### `SetFlags(Channel,Boolean,VisualProxyFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|channel|`Channel`|N/A|
|value|`Boolean`|N/A|
|flagsToChange|`VisualProxyFlags`|N/A|

---
#### `SetFlags(Boolean,VisualFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Boolean`|N/A|
|flags|`VisualFlags`|N/A|

---
#### `SetFlagsOnAllChannels(Boolean,VisualProxyFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Boolean`|N/A|
|flagsToChange|`VisualProxyFlags`|N/A|

---
#### `SetFlagsToRoot(Boolean,VisualFlags)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Boolean`|N/A|
|flag|`VisualFlags`|N/A|

---
#### `SetIsSelfInheritanceParent()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `SetMutableDefaultValue(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Object`|N/A|

---
#### `SetPersistId(Int32)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`Int32`|N/A|

---
#### `SetValue(DependencyProperty,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Boolean`|N/A|

---
#### `SetValue(DependencyPropertyKey,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyPropertyKey`|N/A|
|value|`Boolean`|N/A|

---
#### `SetValueInternal(DependencyProperty,Object)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|
|value|`Object`|N/A|

---
#### `ShouldProvideInheritanceContext(DependencyObject,DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|target|`DependencyObject`|N/A|
|property|`DependencyProperty`|N/A|

---
#### `ShouldSerializeProperty(DependencyProperty)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|dp|`DependencyProperty`|N/A|

---
#### `StartListeningSynchronizedInput(SynchronizedInputType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|inputType|`SynchronizedInputType`|N/A|

---
#### `SynchronizedInputPostOpportunityHandler(Object,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `SynchronizedInputPreOpportunityHandler(Object,RoutedEventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|args|`RoutedEventArgs`|N/A|

---
#### `SynchronizeInheritanceParent(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|parent|`DependencyObject`|N/A|

---
#### `SynchronizeReverseInheritPropertyFlags(DependencyObject,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|oldParent|`DependencyObject`|N/A|
|isCoreParent|`Boolean`|N/A|

---
#### `TransformChanged(Object,EventArgs)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|sender|`Object`|N/A|
|e|`EventArgs`|N/A|

---
#### `TransformToOuterSpace()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`GeneralTransform`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `TrySimpleTransformToAncestor(Visual,Boolean,GeneralTransform&,Matrix&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ancestor|`Visual`|N/A|
|inverse|`Boolean`|N/A|
|generalTransform|`GeneralTransform&`|N/A|
|simpleTransform|`Matrix&`|N/A|

---
#### `TrySimpleTransformToAncestor(Visual3D,GeneralTransform2DTo3D&)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Boolean`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|ancestor|`Visual3D`|N/A|
|transformTo3D|`GeneralTransform2DTo3D&`|N/A|

---
#### `UnsetEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|

---
#### `UpdateEffectiveValue(EntryIndex,DependencyProperty,PropertyMetadata,EffectiveValueEntry,EffectiveValueEntry&,Boolean,Boolean,OperationType)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`UpdateResult`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|entryIndex|`EntryIndex`|N/A|
|dp|`DependencyProperty`|N/A|
|metadata|`PropertyMetadata`|N/A|
|oldEntry|`EffectiveValueEntry`|N/A|
|newEntry|`EffectiveValueEntry&`|N/A|
|coerceWithDeferredReference|`Boolean`|N/A|
|coerceWithCurrentValue|`Boolean`|N/A|
|operationType|`OperationType`|N/A|

---
#### `UpdateIsVisibleCache()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateStyleProperty()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateThemeStyleProperty()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateVisualState()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `UpdateVisualState(Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|useTransitions|`Boolean`|N/A|

---
#### `VerifyAPIReadOnly()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `VerifyAPIReadOnly(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`DependencyObject`|N/A|

---
#### `VerifyAPIReadWrite()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---
#### `VerifyAPIReadWrite(DependencyObject)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|value|`DependencyObject`|N/A|

---
#### `WalkContent(DrawingContextWalker)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|walker|`DrawingContextWalker`|N/A|

---
#### `WriteControlFlag(ControlBoolFlags,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`ControlBoolFlags`|N/A|
|set|`Boolean`|N/A|

---
#### `WriteFlag(CoreFlags,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|field|`CoreFlags`|N/A|
|value|`Boolean`|N/A|

---
#### `WriteInternalFlag(InternalFlags,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`InternalFlags`|N/A|
|set|`Boolean`|N/A|

---
#### `WriteInternalFlag2(InternalFlags2,Boolean)`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|reqFlag|`InternalFlags2`|N/A|
|set|`Boolean`|N/A|

---
#### `ZoomToPreviousExtentCore()`

**Summary**

   *N/A*

**Remarks**

   *N/A*

**Return Value**

|Type|Description|
|---|---|
|`Void`|N/A|

**Parameters**

|Name|Type|Description|
|---|---|---|
|N/A|N/A|N/A|

---

### Public Events

#### MapClick

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)

#### MapTap

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapTapMapViewEventArgs`](ThinkGeo.UI.Wpf.MapTapMapViewEventArgs.md)

#### MapDoubleClick

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`MapClickMapViewEventArgs`](ThinkGeo.UI.Wpf.MapClickMapViewEventArgs.md)

#### CurrentExtentChanging

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CurrentExtentChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangingMapViewEventArgs.md)

#### ZoomLevelSetChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`ZoomLevelSetChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.ZoomLevelSetChangedMapViewEventArgs.md)

#### CurrentExtentChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CurrentExtentChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentExtentChangedMapViewEventArgs.md)

#### CurrentScaleChanging

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CurrentScaleChangingMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangingMapViewEventArgs.md)

#### CurrentScaleChanged

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`CurrentScaleChangedMapViewEventArgs`](ThinkGeo.UI.Wpf.CurrentScaleChangedMapViewEventArgs.md)

#### OverlayDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OverlayDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawingMapViewEventArgs.md)

#### OverlayDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OverlayDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlayDrawnMapViewEventArgs.md)

#### OverlaysDrawing

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OverlaysDrawingMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawingMapViewEventArgs.md)

#### OverlaysDrawn

*N/A*

**Remarks**

*N/A*

**Event Arguments**

[`OverlaysDrawnMapViewEventArgs`](ThinkGeo.UI.Wpf.OverlaysDrawnMapViewEventArgs.md)

#### TargetUpdated

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`DataTransferEventArgs`

#### SourceUpdated

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`DataTransferEventArgs`

#### PreviewTouchDown

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### TouchDown

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### PreviewTouchMove

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### TouchMove

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### PreviewTouchUp

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### TouchUp

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### GotTouchCapture

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### LostTouchCapture

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### TouchEnter

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### TouchLeave

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`TouchEventArgs`

#### ManipulationStarting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationStartingEventArgs`

#### ManipulationStarted

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationStartedEventArgs`

#### ManipulationDelta

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationDeltaEventArgs`

#### ManipulationInertiaStarting

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationInertiaStartingEventArgs`

#### ManipulationBoundaryFeedback

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationBoundaryFeedbackEventArgs`

#### ManipulationCompleted

*N/A*

**Remarks**

*N/A*

**Event Arguments**

`ManipulationCompletedEventArgs`


