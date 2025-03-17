/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  experimental: {
    appDir: true,
  },
  // Make sure images from external sources are allowed if you're using them
  images: {
    domains: [], // Add any external domains here
  },
};

module.exports = nextConfig;
