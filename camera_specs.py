import piexif
from PIL import Image

# Sensor specifications
SENSOR_WIDTH_MM = 9.0
SENSOR_HEIGHT_MM = 7.0
DEFAULT_FOCAL_LENGTH_MM = 5.7  # Fallback value if EXIF data is unavailable


def get_image_size(image_path):
    """Returns the width and height of the image."""
    try:
        with Image.open(image_path) as img:
            return img.size  # (width, height)
    except Exception as e:
        print(f"Error opening image {image_path}: {e}")
        return None, None


def get_focal_length(image_path):
    """Extracts focal length from EXIF metadata of the given image."""
    try:
        exif_data = piexif.load(image_path)
        exif_ifd = exif_data.get("Exif", {})
        focal_length_tag = 0x920A

        if focal_length_tag in exif_ifd:
            numerator, denominator = exif_ifd[focal_length_tag]
            return numerator / denominator if denominator else None
    except Exception as e:
        print(f"Error reading EXIF data from {image_path}: {e}")
    return None


def compute_focal_length():
    """Computes the final focal length based on available image data."""
    focal_front = get_focal_length("front_view.jpg")
    focal_side = get_focal_length("side_view.jpg")

    if focal_front is None and focal_side is None:
        return DEFAULT_FOCAL_LENGTH_MM
    elif focal_front is None:
        return focal_side
    elif focal_side is None:
        return focal_front
    return (focal_front + focal_side) / 2


# Get image dimensions from front view image
IMAGE_WIDTH_PIXELS, IMAGE_HEIGHT_PIXELS = get_image_size("front_view.jpg")
if IMAGE_WIDTH_PIXELS is None or IMAGE_HEIGHT_PIXELS is None:
    IMAGE_WIDTH_PIXELS, IMAGE_HEIGHT_PIXELS = 6048, 4524  # Default values

# Compute focal length values
FOCAL_LENGTH_MM = compute_focal_length()
FOCAL_LENGTH_X_PIXELS = (FOCAL_LENGTH_MM / SENSOR_WIDTH_MM) * IMAGE_WIDTH_PIXELS
FOCAL_LENGTH_Y_PIXELS = (FOCAL_LENGTH_MM / SENSOR_HEIGHT_MM) * IMAGE_HEIGHT_PIXELS

# Output results
print(f"Image Resolution: {IMAGE_WIDTH_PIXELS}x{IMAGE_HEIGHT_PIXELS}")
print(f"Computed Focal Length (mm): {FOCAL_LENGTH_MM:.2f}")
print(f"Focal Length in X Pixels: {FOCAL_LENGTH_X_PIXELS:.2f}")
print(f"Focal Length in Y Pixels: {FOCAL_LENGTH_Y_PIXELS:.2f}")
