import Home from '../App.vue'
import { createMemoryHistory, createRouter } from 'vue-router'

const routes = [
	{ path: '/', component: Home },
];

const router = createRouter({
	history: createMemoryHistory(),
	routes,
})

export default router;
