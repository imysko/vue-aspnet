import {onBeforeMount, reactive, ref} from "vue"
import {defineStore} from "pinia"
import axios from "axios"
import _ from "lodash"

const useAuthenticationStore = defineStore("authenticationStore", () => {
    const user = reactive({
        username: String,
        roles: []
    })
    const checkAuth = ref(false)

    onBeforeMount(async () => {
        await checkAuthentication()
        if (checkAuth.value) {
            await getUser()
        }
    })

    async function registration(user) {
        try {
            let response = await axios.post('api/authentication/register', user)
            return {
                status_code: response.status,
                message: response.data.message
            }
        }
        catch (e) {
            return {
                status_code: e.response.status,
                message: e.response.data
            }
        }
    }

    async function login(user) {
        try {
            let response = await axios.post('api/authentication/login', user)
            await getUser()

            return {
                status_code: response.status,
                message: response.data.message
            }
        }
        catch (e) {
            return {
                status_code: e.response.status,
                message: e.response.data
            }
        }
    }

    async function logout() {
        try {
            let response = await axios.post('api/authentication/logout')
            await getUser()

            return {
                status_code: response.status,
                message: response.data.message
            }
        }
        catch (e) {
            return {
                status_code: e.response.status,
                message: e.response.data
            }
        }
    }

    async function getUser() {
        await checkAuthentication()

        if (checkAuth.value) {
            try {
                let response = (await axios.get('api/authentication/me'))

                user.username = response.data.username
                user.roles = response.data.roles

                return {
                    status_code: response.status,
                    message: response.data.message
                }
            }
            catch (e) {
                user.username = undefined
                user.roles = []

                return {
                    status_code: e.response.status,
                    message: e.response.data
                }
            }
        }
        else {
            user.username = undefined
            user.roles = []
        }
    }

    async function checkAuthentication() {
        checkAuth.value = (await axios.get('api/authentication/check_authentication')).data
    }

    function canAction(action) {
        return _.includes(user.roles, action) || _.includes(user.roles, 'superuser')
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