/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: true,
  swcMinify: true,
  env: {
    AVERBOT_API_URI: process.env.AVERBOT_API_URI,
    AVERBOT_API_HUB_WARNS: process.env.AVERBOT_API_HUB_WARNS
  }
}

module.exports = nextConfig
