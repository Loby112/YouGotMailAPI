const baseUrl = "https://yougotmailv2.azurewebsites.net/api/Email"

Vue.createApp({
    created() {
        this.getAllEmail()
    },
    data() {
        return {
            emails: [],
            email: null,
            deleteMessage: "",
            deleteId: 0,
            addMessage: "",
            addData: { id: 0, toEmails: "", subject: "", body: "" }
        }
    },

    methods: {

        async helperGetAndShow(url) { // helper metode: getAllBooks + getByTitle are very similar
            try {
                const response = await axios.get(url)
                this.emails = await response.data
            } catch (ex) {
                alert(ex.message) // https://www.w3schools.com/js/js_popup.asp
            }
        },

        getAllEmail() {
            this.helperGetAndShow(baseUrl)
        },

        async addEmail() {
            
                response = await axios.post(baseUrl, this.addData)
                //this.addMessage = "response " + response.status + " " + response.statusText
                this.getAllEmail()
        },


        async deleteEmail(deleteId) {
            const url = baseUrl + "/" + deleteId

            try {
                response = await axios.delete(url)
                this.deleteMessage = response.status + " " + response.statusText
                this.getAllEmail()
            } catch (ex) {
                alert(ex.message)
            }
            
        }
    },
}).mount("#email")