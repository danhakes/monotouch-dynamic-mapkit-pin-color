using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.CoreAnimation;

namespace MapSupport
{
	public class CustomPinColorAnnotation
	{
		/// <summary>
		/// Creates a map annotation pin image with color described by the RGB values.
		/// </summary>
		/// <returns>
		/// The annotation pin image.
		/// </returns>
		/// <param name='red'>
		///  The red interger value
		/// </param>
		/// <param name='green'>
		///  The green interger value.
		/// </param>
		/// <param name='blue'>
		///  The blue interger value
		/// </param>
		/// <param name='scale'>
		///  The screen scale
		/// </param>
		/// <param name='imageFolderPath'>
		///  The relative path to the image folder. Do not include ending /.
		/// </param>
		public static UIImage GetPinImageForRGB (int red, int green, int blue, float scale, string imageFolderPath)
		{
			UIColor color = UIColor.FromRGB(red, green, blue);
			return GetPinImageForColor(color, scale, imageFolderPath);
		}

		/// <summary>
		/// Creates a map annotation pin image with UIColor provided.
		/// </summary>
		/// <returns>
		/// A map annotation pin image.
		/// </returns>
		/// <param name='color'>
		///  The desired color for the pin image.
		/// </param>
		/// <param name='scale'>
		///  The Screen scale
		/// </param>
		/// <param name='imageFolderPath'>
		///  The relative path to the image folder. Do not include ending /.
		/// </param>
		public static UIImage GetPinImageForColor(UIColor color, float scale, string imageFolderPath)
		{
			// Get ball image colored with desired color
			UIImage ballImage = GetBallImageForColor(color, scale, imageFolderPath);
			// Create shadow and stick images
			UIImage shadowImage = new UIImage(string.Format(IMAGE_PATH_TEMPLATE, imageFolderPath, IMAGE_PIN_SHADOW));
			UIImage stickImage = new UIImage(string.Format(IMAGE_PATH_TEMPLATE, imageFolderPath, IMAGE_PIN_STICK));
			// Start image context
			UIGraphics.BeginImageContextWithOptions(stickImage.Size, false, scale);
			// Get stick rectangle
			RectangleF rect = new RectangleF(0, 0, stickImage.Size.Width, stickImage.Size.Height);
			// Combine images using stick rectangle
			ballImage.Draw(rect);
			shadowImage.Draw(rect);
			stickImage.Draw(rect);
			// Get the composit image from context
			UIImage pinImage = UIGraphics.GetImageFromCurrentImageContext();
			// End the context
			UIGraphics.EndImageContext();
			
			return pinImage;
		}

		/// <summary>
		/// Creates the map annotation pin ball part of the pin image in the UIColor provided.
		/// </summary>
		/// <returns>
		/// A map annotation pin ball image.
		/// </returns>
		/// <param name='color'>
		///  The desired color for the pin ball image.
		/// </param>
		/// <param name='scale'>
		///  The Screen scale
		/// </param>
		/// <param name='imageFolderPath'>
		///  The relative page to the image folder. Do not include ending /.
		/// </param>
		private static UIImage GetBallImageForColor(UIColor color, float scale, string imageFolderPath)
		{
			// Get the blank pin ball image
			UIImage baseImage = new UIImage(string.Format(IMAGE_PATH_TEMPLATE, imageFolderPath, IMAGE_PIN_BALL));
			// Start and get the image context
			UIGraphics.BeginImageContextWithOptions(baseImage.Size, false, scale);
			CGContext context = UIGraphics.GetCurrentContext();
			// Set fill color to the passed in color
			color.SetFill();
			// Translate and flip the graphic context
			context.TranslateCTM(0, baseImage.Size.Height);
			context.ScaleCTM(1.0f, -1.0f);
			// Set the blend mode
			context.SetBlendMode(CGBlendMode.Normal);
			RectangleF rect = new RectangleF(0, 0, baseImage.Size.Width, baseImage.Size.Height);
			context.DrawImage(rect, baseImage.CGImage);
			// Create and set mask matching shape of the image.
			context.ClipToMask(rect, baseImage.CGImage);
			context.AddRect(rect);
			// paint the ball
			context.DrawPath(CGPathDrawingMode.Fill);
			// Generate image from context
			UIImage colorImage = UIGraphics.GetImageFromCurrentImageContext();
			// End the context
			UIGraphics.EndImageContext();
			
			return colorImage;
		}
	}
}

