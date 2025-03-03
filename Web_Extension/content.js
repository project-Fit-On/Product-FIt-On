// Function to extract product details from the provided structure
function extractProductDetails() {
  let productName = "";
  const titleElement = document.querySelector("h1.product_title.entry-title");
  if (titleElement) {
    productName = titleElement.textContent.trim();
  }

  let productImage = "";
  const imageElement = document.querySelector(
    ".woocommerce-product-gallery__image a img"
  );
  if (imageElement && imageElement.getAttribute("data-src")) {
    productImage = imageElement.getAttribute("data-src");
  } else if (imageElement && imageElement.src) {
    productImage = imageElement.src;
  }

  let productDesc = "";
  const descElement = document.querySelector(
    ".woocommerce-product-details__short-description"
  );
  if (descElement) {
    productDesc = descElement.textContent.trim();
  }

  let productSizes = [];
  const sizeElements = document.querySelectorAll(
    ".single_variation_wrap select option"
  );
  if (sizeElements.length > 0) {
    productSizes = Array.from(sizeElements)
      .map((el) => el.textContent.trim())
      .filter((size) => size.length > 0);
  }

  return {
    product: encodeURIComponent(productName),
    image: encodeURIComponent(productImage),
    desc: encodeURIComponent(productDesc.substring(0, 200)), // Limit description length
    sizes: encodeURIComponent(productSizes.join(",")), // Join sizes as a comma-separated string
  };
}

// Function to create and inject the Try 3D button
function injectTry3DButton() {
  const addToCartButton = document.querySelector(
    ".single_add_to_cart_button.button.alt"
  );

  if (!addToCartButton) {
    console.error(
      "Could not find a suitable location to inject the Try 3D button"
    );
    return;
  }

  // Ensure the button isn't added multiple times
  if (document.querySelector(".try-3d-button")) return;

  // Create the Try 3D button
  const try3DButton = document.createElement("button");
  try3DButton.className = "try-3d-button";
  try3DButton.innerHTML = "Try 3D";
  try3DButton.style.cssText =
    "background: #28a745; color: white; padding: 10px 20px; margin-left: 10px; border: none; cursor: pointer; font-size: 14px; display: block; margin-top: 10px;";

  try3DButton.addEventListener("click", function (event) {
    event.preventDefault();
    const productDetails = extractProductDetails();
    const redirectURL = `https://myfiton.com/try3d?product=${productDetails.product}&image=${productDetails.image}&desc=${productDetails.desc}&sizes=${productDetails.sizes}`;
    window.location.href = redirectURL;
  });

  // Insert the button next to the Add to Cart button
  addToCartButton.parentNode.insertBefore(
    try3DButton,
    addToCartButton.nextSibling
  );
  console.log("Try 3D button injected successfully");
}

// Function to wait for elements using MutationObserver
function observeAndInjectButton() {
  const observer = new MutationObserver((mutations, observerInstance) => {
    const addToCartButton = document.querySelector(
      ".single_add_to_cart_button.button.alt"
    );

    if (addToCartButton) {
      injectTry3DButton();
      observerInstance.disconnect(); // Stop observing once button is added
    }
  });

  observer.observe(document.body, { childList: true, subtree: true });
}

// Initialize when the page is fully loaded
window.addEventListener("load", function () {
  console.log("Page loaded, trying to inject button...");
  setTimeout(() => {
    if (!document.querySelector(".try-3d-button")) {
      injectTry3DButton();
    }
  }, 2000); // Wait 2 seconds for dynamic content

  observeAndInjectButton(); // Start observing for dynamic elements
});
