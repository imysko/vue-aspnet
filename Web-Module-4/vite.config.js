import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    proxy: {
      '^/api': {
        target: 'https://localhost:7014',
        changeOrigin: true,
        secure: false
      }
    }
  },
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://localhost:7014',
        changeOrigin: true,
        secure: false
      }
    }
  }
})