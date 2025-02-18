// Listen for installation event.
chrome.runtime.onInstalled.addListener(function () {
  console.log("3D Try-On Extension installed successfully");
});

// Optional: Listen for messages from content script
chrome.runtime.onMessage.addListener(function (request, sender, sendResponse) {
  if (request.action === "productRedirect") {
    console.log(
      "Redirecting to try-on page with product:",
      request.productDetails
    );
    // You can perform additional actions here if needed
    sendResponse({ status: "success" });
  }
  return true; // Required for asynchronous response
});

// Optional: Track navigation to your site after clicking Try 3D button
chrome.tabs.onUpdated.addListener(function (tabId, changeInfo, tab) {
  if (
    changeInfo.status === "complete" &&
    tab.url &&
    tab.url.includes("your-site.com/try3d")
  ) {
    console.log("User landed on try3d page:", tab.url);
  }
});
