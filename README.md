monotouch-dynamic-mapkit-pin-color
==================================

To add addition colors for Annotation pins other than the three Annotation pin colors provided by Apple in the MapKit you had to create a new image for each color. That is no longer the case. nnhubbard provided an objective-C solution with ZSPinAnnotation (https://github.com/nnhubbard/ZSPinAnnotation) that allows you to dynamically create a new colored pin for any UIColor. This is the c# version of that for monotouch. I've enhanced it a little so you can send in RGB values as well as a UIColor. 

Simply include the images and MapSupport.cs file in your MonoTouch project. Override the GetViewForAnnotation method in the MKMapViewDelegate and set the MKAnnotationView.Image as follows:

[With RGB]

MKAnnotationView.Image = MapSupport.GetPinImageForRGB(242, 196, 26, UIScreen.MainScreen.Scale, _mapsImagesPath);

[With UIColor]

MKAnnotationView.Image = MapSupport.GetPinImageForColor(UIColor.Yellow, UIScreen.MainScreen.Scale, _mapsImagesPath);
