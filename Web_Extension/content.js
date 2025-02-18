// Function to extract product details based on partner website
function extractProductDetails() {
  // This will need customization based on partner site structure
  // The selectors below are examples and should be adjusted for each partner

  // Extract product name
  let productName = "";
  const titleElement =
    document.querySelector("h1.product-title") ||
    document.querySelector(".product-name") ||
    document.querySelector('[data-testid="product-title"]');
  if (titleElement) {
    productName = titleElement.textContent.trim();
  }

  // Extract product image
  let productImage = "";
  const imageElement =
    document.querySelector(".product-image img") ||
    document.querySelector(".main-product-image") ||
    document.querySelector('[data-testid="product-image"]');
  if (imageElement && imageElement.src) {
    productImage = imageElement.src;
  }

  // Extract product description
  let productDesc = "";
  const descElement =
    document.querySelector(".product-description") ||
    document.querySelector("#product-description") ||
    document.querySelector('[data-testid="product-description"]');
  if (descElement) {
    productDesc = descElement.textContent.trim();
  }

  return {
    product: encodeURIComponent(productName),
    image: encodeURIComponent(productImage),
    desc: encodeURIComponent(productDesc.substring(0, 200)), // Limit description length
  };
}

// Function to create and inject the Try 3D button
function injectTry3DButton() {
  // Find a suitable location to inject the button
  // This will vary by partner site, so adapt as needed
  const addToCartButton =
    document.querySelector(".add-to-cart") ||
    document.querySelector("#add-to-cart") ||
    document.querySelector('[data-testid="add-to-cart-button"]');

  if (!addToCartButton) {
    console.error(
      "Could not find a suitable location to inject the Try 3D button"
    );
    return;
  }

  // Create the Try 3D button
  const try3DButton = document.createElement("button");
  try3DButton.className = "try-3d-button";
  try3DButton.innerHTML = "Try 3D";

  // Add click event listener
  try3DButton.addEventListener("click", function (event) {
    event.preventDefault();

    // Extract product details
    const productDetails = extractProductDetails();

    // Construct the URL for redirection
    const redirectURL = `https://your-site.com/try3d?product=${productDetails.product}&image=${productDetails.image}&desc=${productDetails.desc}`;

    // Redirect to your site
    window.location.href = redirectURL;
  });

  // Insert the button next to the Add to Cart button
  addToCartButton.parentNode.insertBefore(
    try3DButton,
    addToCartButton.nextSibling
  );
}

// Initialize when the page is fully loaded
window.addEventListener("load", function () {
  setTimeout(injectTry3DButton, 1000); // Small delay to ensure all elements are loaded
});
