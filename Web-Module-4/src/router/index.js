import { createRouter, createWebHistory } from 'vue-router'
import useAuthenticationStore from "@/store/authenticationStore"

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('@/views/HomeView.vue')
    },
    {
      path: '/car/:id',
      name: 'car-page',
      component: () => import('@/views/CarView.vue'),
      params: true,
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'not-found',
      component: () => import('@/views/NotFound.vue')
    },
    {
      path: '/authorization',
      name: 'authorization',
      component: () => import('@/views/AuthorizationView.vue'),
      meta: {
        authorization: false
      }
    },
    {
      path: '/registration',
      name: 'registration',
      component: () => import('@/views/RegistrationView.vue'),
      meta: {
        authorization: false
      }
    },
    {
      path: '/search',
      name: 'search',
      component: () => import('@/views/SearchView.vue')
    }
  ]
})

router.beforeEach(async (to, from, next) => {
  const authStore = await useAuthenticationStore()

  if (to.meta.authorization !== undefined && authStore.checkAuth !== to.meta.authorization) {
    next({
      path: "/"
    })
    return
  }
  next()
})

export default router
