import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'
import BootstrapVue from "bootstrap-vue-3"
import { FontAwesomeIcon} from "@fortawesome/vue-fontawesome"
import { library} from "@fortawesome/fontawesome-svg-core"
import { fas } from "@fortawesome/free-solid-svg-icons"

import "bootstrap/dist/css/bootstrap.css"
import "bootstrap-vue-3/dist/bootstrap-vue-3.css"
import "fontawesome-free/css/all.css"

library.add(fas)

const pinia = createPinia()
const app = createApp(App)

app.use(pinia)
app.use(router)
app.use(BootstrapVue)

app.component('font-awesome-icon', FontAwesomeIcon)

app.mount('#app')
