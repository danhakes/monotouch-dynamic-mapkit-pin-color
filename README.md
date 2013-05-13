monotouch-dynamic-mapkit-pin-color
==================================

You don't have to be limited to the three Annotation pin colors provided by Apple in the MapKit. nnhubbard provided an objective-C solution with ZSPinAnnotation (https://github.com/nnhubbard/ZSPinAnnotation). This is the c# version for monotouch. I've enhanced it so you can send in RGB values as well as a UIColor. 

Simply include the images and MapSupport.cs file in your MonoTouch project. Override the GetViewForAnnotation method and set the MKAnnotationView.Image as follows:

[With RGB]

MKAnnotationView.Image = MapSupport.GetPinImageForRGB(242, 196, 26, UIScreen.MainScreen.Scale, _mapsImagesPath);

[With UIColor]

MKAnnotationView.Image = MapSupport.GetPinImageForColor(UIColor.Yellow, UIScreen.MainScreen.Scale, _mapsImagesPath);
