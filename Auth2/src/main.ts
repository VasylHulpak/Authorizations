import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import 'virtual:windi.css'
import 'virtual:windi-devtools'

createApp(App).use(router).mount('#app')
