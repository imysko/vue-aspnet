import {onBeforeMount, ref} from "vue"
import {defineStore} from "pinia"
import axios from "axios"
import _ from "lodash"

const useAuthenticationStore = defineStore("authenticationStore", () => {
    const permissions = ref([])
    const checkAuth = ref(false)

    onBeforeMount(async () => {
        await checkAuthentication()
        await getRoles()
    })

    async function registration(user) {
        let request = await axios.post('api/authentication/register', user)

        return {
            result: request.data.result,
            message: request.data.message
        }
    }

    async function login(user) {
        let request = await axios.post('api/authentication/login', user)
        await checkAuthentication()
        await getRoles()

        return {
            result: request.data.result,
            message: request.data.message
        }
    }

    async function logout() {
        let request = await axios.post('api/authentication/logout')
        await checkAuthentication()
        await getRoles()

        return {
            result: request.data.result,
            message: request.data.message
        }
    }

    async function checkAuthentication() {
        checkAuth.value = (await axios.get('api/authentication/check_authentication')).data
    }

    async function getRoles() {
        permissions.value = (await axios.get('api/user/roles')).data
    }

    function canAction(action) {
        return _.includes(permissions.value, action) || _.includes(permissions.value, 'Superuser')
    }

    return {
        registration,
        login,
        logout,
        canAction,
        checkAuth
    }
})

export default useAuthenticationStore