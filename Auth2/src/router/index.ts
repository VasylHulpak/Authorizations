import Home from '../App.vue'
import CallbackComponent from '../pages/Callback.vue'
import { createMemoryHistory, createRouter } from 'vue-router'

const routes = [
	{ path: '/', component: Home },
	{ path: '/callback', component: CallbackComponent },
];

const router = createRouter({
	history: createMemoryHistory(),
	routes,
})

export default router;
