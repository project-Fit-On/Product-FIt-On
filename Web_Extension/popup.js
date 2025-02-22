// Update status when popup is opened
document.addEventListener("DOMContentLoaded", function () {
  // Check if we're on a supported partner site
  chrome.tabs.query({ active: true, currentWindow: true }, function (tabs) {
    const currentUrl = tabs[0].url;
    const statusElement = document.getElementById("status");

    if (
      currentUrl.includes("fashionbug.lk") ||
      currentUrl.includes("partner-site2.com")
    ) {
      statusElement.textContent =
        'Status: You are on a supported partner site. Look for the "Try 3D" button on product pages.';
      statusElement.style.color = "#4CAF50";
    } else if (currentUrl.includes("your-site.com")) {
      statusElement.textContent = "Status: You are on the 3D Try-On website.";
      statusElement.style.color = "#2196F3";
    } else {
      statusElement.textContent =
        "Status: Not a supported partner site. Visit one of our partner sites to use the Try 3D feature.";
      statusElement.style.color = "#FF5722";
    }
  });
});
