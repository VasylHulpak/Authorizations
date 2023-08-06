import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import WindiCSS from 'vite-plugin-windicss'

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [
		vue(),
		WindiCSS({
			scan: {
				dirs: ['.'], // all files in the cwd
				fileExtensions: ['vue', 'js', 'ts'] // also enabled scanning for js/ts
			}
		})
	],
	port: 5250,
	proxy: {
		'^(/api)': {
			target: 'https://localtest.me:5250/',
			changeOrigin: true,
			secure: false,
			ws: true
		}
	},
	open: true
})
