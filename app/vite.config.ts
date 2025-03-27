import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vite';

export default defineConfig({
 // base: '/your-subdirectory/', // Set this if deploying in a subdirectory
  plugins: [sveltekit()],
  build: {
    target: 'esnext', // Target modern browsers
    outDir: 'dist', // Output directory for production files
    sourcemap: false, // Turn off sourcemaps for production
    minify: 'terser', // Minify the code using Terser for production
    rollupOptions: {
      output: {
        // Adjust chunking and other settings if necessary
      },
    },
  },
});
