// ClothingCRM Application with Backend Integration

class ClothingCRM {
    constructor() {
        // Configure your backend API base URL
        this.API_BASE_URL = "https://localhost:7061/api"; // Change this to your backend URL

        this.products = [];
        this.customers = [];
        this.orders = [];
        this.currentPage = "dashboard";
        this.editingItem = null;
        this.editingType = null;

        this.init();
    }

    // API Configuration and Helper Methods
    async apiRequest(endpoint, options = {}) {
        const url = `${this.API_BASE_URL}${endpoint}`;
        const config = {
            headers: {
                "Content-Type": "application/json",
                // Add authorization header if needed
                // 'Authorization': `Bearer ${this.getAuthToken()}`,
                ...options.headers,
            },
            ...options,
        };

        try {
            const response = await fetch(url, config);

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const data = await response.json();
            return data;
        } catch (error) {
            console.error("API Request failed:", error);
            this.showNotification(`Error: ${error.message}`, "error");
            throw error;
        }
    }

    // Authentication (if needed)
    getAuthToken() {
        return localStorage.getItem("auth_token");
    }

    setAuthToken(token) {
        localStorage.setItem("auth_token", token);
    }

    // API Methods for Products
    async fetchProducts() {
        try {
            const data = await this.apiRequest("/products");
            this.products = data;
            return data;
        } catch (error) {
            // Fallback to empty array on error
            this.products = [];
            return [];
        }
    }

    async createProduct(productData) {
        const data = await this.apiRequest("/Products", {
            method: "POST",
            body: JSON.stringify(productData),
        });
        return data;
    }

    async updateProduct(id, productData) {
        const data = await this.apiRequest(`/Products/${id}`, {
            method: "PUT",
            body: JSON.stringify(productData),
        });
        return data;
    }

    async deleteProduct(id) {
        const data = await this.apiRequest(`/Products/${id}`, {
            method: "DELETE",
        });
        return data;
    }

    // API Methods for Customers
    async fetchCustomers() {
        try {
            const data = await this.apiRequest("/Customers");
            this.customers = data;
            return data;
        } catch (error) {
            this.customers = [];
            return [];
        }
    }

    async createCustomer(customerData) {
        const data = await this.apiRequest("/Customers", {
            method: "POST",
            body: JSON.stringify(customerData),
        });
        return data;
    }

    async updateCustomer(id, customerData) {
        const data = await this.apiRequest(`/Customers/${id}`, {
            method: "PUT",
            body: JSON.stringify(customerData),
        });
        return data;
    }

    async deleteCustomerAPI(id) {
        const data = await this.apiRequest(`/Customers/${id}`, {
            method: "DELETE",
        });
        return data;
    }

    // API Methods for Orders
    async fetchOrders() {
        try {
            const data = await this.apiRequest("/Orders");
            this.orders = data;
            return data;
        } catch (error) {
            this.orders = [];
            return [];
        }
    }

    async createOrder(orderData) {
        const data = await this.apiRequest("/Orders", {
            method: "POST",
            body: JSON.stringify(orderData),
        });
        return data;
    }

    async updateOrder(id, orderData) {
        const data = await this.apiRequest(`/Orders/${id}`, {
            method: "PUT",
            body: JSON.stringify(orderData),
        });
        return data;
    }

    async deleteOrderAPI(id) {
        const data = await this.apiRequest(`/Orders/${id}`, {
            method: "DELETE",
        });
        return data;
    }

    // Dashboard Analytics API
    async fetchDashboardData() {
        try {
            const data = await this.apiRequest("/Dashboard");
            return data;
        } catch (error) {
            // Fallback to calculated data
            return this.calculateDashboardData();
        }
    }

    async fetchSalesReport() {
        try {
            const data = await this.apiRequest("/Orders");
            calculateSalesReport(data);
            return this.calculateSalesReport(data); // Use real data
        } catch (error) {
            console.warn("API failed, using fallback calculation.");
            return this.calculateSalesReport(this.orders || []); // fallback to local orders array
        }
    }

    calculateSalesReport(orders) {
        let totalRevenue = 0;
        let totalProductsSold = 0;
        let customerIds = new Set();

        for (const order of orders) {
            if (order.status === "Completed") {
                totalRevenue += order.totalAmount || 0;
                customerIds.add(order.customerId);

                // Sum quantity from order items
                for (const item of order.orderItems || []) {
                    totalProductsSold += item.quantity || 0;
                }
            }
        }

        const completedOrdersCount = orders.filter(
            (o) => o.status === "Completed"
        ).length;
        const averageOrderValue = completedOrdersCount
            ? totalRevenue / completedOrdersCount
            : 0;

        return {
            totalRevenue: `${totalRevenue.toFixed(2)}`,
            averageOrderValue: `${averageOrderValue.toFixed(2)}`,
            totalProductsSold,
            activeCustomers: customerIds.size,
        };
    }

    // Initialize Application
    async init() {
        this.setupEventListeners();
        this.showLoadingState();

        try {
            // Load initial data from backend
            await Promise.all([
                this.fetchProducts(),
                this.fetchCustomers(),
                this.fetchOrders(),
            ]);

            this.hideLoadingState();
            this.showPage("dashboard");
            this.updateDashboard();
        } catch (error) {
            this.hideLoadingState();
            this.showNotification("Failed to load data from server", "error");
            // You might want to load fallback data or show an error state
        }
    }

    setupEventListeners() {
        // Navigation
        document.querySelectorAll(".nav-link").forEach((link) => {
            link.addEventListener("click", (e) => {
                e.preventDefault();
                const page = e.currentTarget.getAttribute("data-page");
                this.showPage(page);
            });
        });

        // Forms
        // document
        //     .getElementById("product-form")
        //     .addEventListener("submit", (e) => this.handleProductSubmit(e));
        // document
        //     .getElementById("customer-form")
        //     .addEventListener("submit", (e) => this.handleCustomerSubmit(e));
        // document
        //     .getElementById("edit-form")
        //     .addEventListener("submit", (e) => this.handleEditSubmit(e));

        // Cancel buttons
        // document
        //     .getElementById("cancel-product")
        //     .addEventListener("click", () => this.clearProductForm());
        // document
        //     .getElementById("cancel-customer")
        //     .addEventListener("click", () => this.clearCustomerForm());

        // Search
        document
            .querySelector(".search-input")
            .addEventListener("input", (e) =>
                this.handleSearch(e.target.value)
            );
    }

    showLoadingState() {
        // Add loading spinner or skeleton UI
        document.body.classList.add("loading");
    }

    hideLoadingState() {
        document.body.classList.remove("loading");
    }

    async showPage(pageId) {
        // Update navigation
        document.querySelectorAll(".nav-link").forEach((link) => {
            link.classList.remove("active");
        });
        document
            .querySelector(`[data-page="${pageId}"]`)
            .classList.add("active");

        // Update content
        document.querySelectorAll(".content-page").forEach((page) => {
            page.classList.remove("active");
        });
        document.getElementById(pageId).classList.add("active");

        this.currentPage = pageId;

        // Load page-specific data
        try {
            switch (pageId) {
                case "dashboard":
                    await this.updateDashboard();
                    break;
                case "products":
                    await this.refreshProducts();
                    break;
                case "customers":
                    await this.refreshCustomers();
                    break;
                case "orders":
                    await this.refreshOrders();
                    break;
                case "sales-report":
                    await this.updateSalesReport();
                    break;
            }
        } catch (error) {
            this.showNotification("Failed to load page data", "error");
        }
    }

    async refreshProducts() {
        await this.fetchProducts();
        this.loadProductsTable();
    }

    async refreshCustomers() {
        await this.fetchCustomers();
        this.loadCustomersTable();
    }

    async refreshOrders() {
        await this.fetchOrders();
        this.loadOrdersTable();
    }

    async updateDashboard() {
        try {
            const dashboardData = await this.fetchDashboardData();

            document.getElementById("todays-sales").textContent =
                dashboardData.todaysSales?.toFixed(2) || "0.00";
            document.getElementById("total-orders").textContent =
                dashboardData.totalOrders || 0;
            document.getElementById("items-sold").textContent =
                dashboardData.itemsSold || 0;
            document.getElementById("new-customers").textContent =
                dashboardData.newCustomers || 0;

            await this.loadTopProducts();
        } catch (error) {
            // Fallback to local calculation
            this.updateDashboardFallback();
        }
    }

    // Fallback calculations when API is unavailable
    calculateDashboardData() {
        const today = new Date().toISOString().split("T")[0];
        const todaysSales = this.orders
            .filter((order) => order.date === today)
            .reduce((sum, order) => sum + order.total, 0);

        return {
            todaysSales,
            totalOrders: this.orders.length,
            itemsSold: this.orders.reduce((sum, order) => sum + order.items, 0),
            newCustomers: this.customers.filter(
                (customer) => customer.created === today
            ).length,
        };
    }

    updateDashboardFallback() {
        const data = this.calculateDashboardData();
        document.getElementById("todays-sales").textContent =
            data.todaysSales.toFixed(2);
        document.getElementById("total-orders").textContent = data.totalOrders;
        document.getElementById("items-sold").textContent = data.itemsSold;
        document.getElementById("new-customers").textContent =
            data.newCustomers;
        this.loadTopProducts();
    }

    async loadTopProducts() {
        const tbody = document.getElementById("top-products-tbody");
        const sortedProducts = [...this.products]
            .sort((a, b) => (b.revenue || 0) - (a.revenue || 0))
            .slice(0, 5);
        
        console.log(sortedProducts);

        tbody.innerHTML = sortedProducts
            .map(
                (product, index) => `
            <tr>
                <td>${index + 1}</td>
                <td>${product.name}</td>
                <td>${product.category}</td>
                <td>${product.salesCount || 0}</td>
                <td>$${(product.revenue || 0).toFixed(2)}</td>
            </tr>
        `
            )
            .join("");
    }

    loadProductsTable() {
        const tbody = document.getElementById("products-tbody");

        const filteredProducts = this.products.filter((product) => {
            // Exclude product if any of these fields are empty, 0, null or undefined
            return (
                product.id != null &&
                product.id !== 0 &&
                product.name &&
                product.name.trim() !== "" &&
                product.category &&
                product.category.trim() !== "" &&
                product.price != null &&
                product.price !== 0 &&
                product.stockQuantity != null &&
                product.stockQuantity !== 0 &&
                product.salesCount != null &&
                product.salesCount !== 0 &&
                product.revenue != null &&
                product.revenue !== 0
            );
        });

        tbody.innerHTML = filteredProducts
            .map(
                (product) => `
            <tr>
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td>${product.category}</td>
                <td>$${product.price.toFixed(2)}</td>
                <td>${product.stockQuantity}</td>
                <td>${product.salesCount}</td>
                <td>$${product.revenue.toFixed(2)}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteProduct(${
                        product.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    loadCustomersTable() {
        const tbody = document.getElementById("customers-tbody");
        tbody.innerHTML = this.customers
            .map(
                (customer) => `
            <tr>
                <td>${customer.id}</td>
                <td>${customer.firstName} ${customer.lastName}</td>
                <td>${customer.email}</td>
                <td>${customer.phone}</td>
                <td>${new Date(customer.createdAt)
                    .toISOString()
                    .split("T")[0]
                    .replace(/-/g, "/")}</td>
                <td>${customer.orders.length || 0}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteCustomer(${
                        customer.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    loadOrdersTable() {
        const tbody = document.getElementById("orders-tbody");

        tbody.innerHTML = this.orders
            .map(
                (order) => `
            <tr>
                <td>${order.id}</td>
                <td>${order.customer.firstName} ${order.customer.lastName}</td>
                <td>${new Date(order.orderDate)
                    .toISOString()
                    .split("T")[0]
                    .replace(/-/g, "/")}</td>
                <td><span class="status status-${order.status.toLowerCase()}">${
                    order.status
                }</span></td>
                <td>$${order.totalAmount.toFixed(2)}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteOrder(${
                        order.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    async updateSalesReport() {
        try {
            const salesData = await this.fetchSalesReport();
            console.log(salesData);

            document.getElementById("total-revenue").textContent =
                salesData.totalRevenue || "0.00";
            document.getElementById("avg-order-value").textContent =
                salesData.averageOrderValue || "0.00";
            document.getElementById("total-products-sold").textContent =
                salesData.totalProductsSold || 0;
            document.getElementById("active-customers").textContent =
                salesData.activeCustomers || 0;
        } catch (error) {
            // Fallback calculation
            const salesData = this.calculateSalesReport();
            document.getElementById("total-revenue").textContent =
                salesData.totalRevenue.toFixed(2);
            document.getElementById("avg-order-value").textContent =
                salesData.avgOrderValue.toFixed(2);
            document.getElementById("total-products-sold").textContent =
                salesData.totalProductsSold;
            document.getElementById("active-customers").textContent =
                salesData.activeCustomers;
        }
    }

    // calculateSalesReport() {
    //     const totalRevenue = this.orders.reduce(
    //         (sum, order) => sum + order.total,
    //         0
    //     );
    //     const avgOrderValue = totalRevenue / this.orders.length || 0;
    //     const totalProductsSold = this.orders.reduce(
    //         (sum, order) => sum + order.items,
    //         0
    //     );
    //     const activeCustomers = this.customers.filter(
    //         (customer) => (customer.orders || 0) > 0
    //     ).length;

    //     return {
    //         totalRevenue,
    //         avgOrderValue,
    //         totalProductsSold,
    //         activeCustomers,
    //     };
    // }

    async handleProductSubmit(e) {
        e.preventDefault();

        try {
            const productData = {
                name: document.getElementById("product-name").value,
                category: document.getElementById("product-category").value,
                price: parseFloat(
                    document.getElementById("product-price").value
                ),
                stock: parseInt(document.getElementById("product-stock").value),
            };

            await this.createProduct(productData);
            await this.refreshProducts();
            this.clearProductForm();
            this.showNotification("Product added successfully!", "success");
        } catch (error) {
            this.showNotification("Failed to add product", "error");
        }
    }

    async handleCustomerSubmit(e) {
        e.preventDefault();

        try {
            const customerData = {
                firstName: document.getElementById("customer-firstname").value,
                lastName: document.getElementById("customer-lastname").value,
                email: document.getElementById("customer-email").value,
                phone: document.getElementById("customer-phone").value,
            };

            await this.createCustomer(customerData);
            await this.refreshCustomers();
            this.clearCustomerForm();
            this.showNotification("Customer added successfully!", "success");
        } catch (error) {
            this.showNotification("Failed to add customer", "error");
        }
    }

    async handleEditSubmit(e) {
        e.preventDefault();
        const formData = new FormData(e.target);

        try {
            if (this.editingType === "product") {
                const productData = {
                    name: formData.get("name"),
                    category: formData.get("category"),
                    price: parseFloat(formData.get("price")),
                    stock: parseInt(formData.get("stock")),
                };
                await this.updateProduct(this.editingItem, productData);
                await this.refreshProducts();
            } else if (this.editingType === "customer") {
                const customerData = {
                    firstName: formData.get("firstName"),
                    lastName: formData.get("lastName"),
                    email: formData.get("email"),
                    phone: formData.get("phone"),
                };
                await this.updateCustomer(this.editingItem, customerData);
                await this.refreshCustomers();
            } else if (this.editingType === "order") {
                const orderData = {
                    status: formData.get("status"),
                    // total: parseFloat(formData.get("total")),
                    // items: parseInt(formData.get("items")),
                };
                await this.updateOrder(this.editingItem, orderData);
                await this.refreshOrders();
            }

            this.closeModal();
            this.showNotification("Item updated successfully!", "success");
        } catch (error) {
            this.showNotification("Failed to update item", "error");
        }
    }

    editProduct(id) {
        const product = this.products.find((p) => p.id === id);
        if (product) {
            this.editingItem = id;
            this.editingType = "product";
            this.openEditModal("Edit Product", [
                {
                    name: "name",
                    label: "Product Name",
                    type: "text",
                    value: product.name,
                },
                {
                    name: "category",
                    label: "Category",
                    type: "select",
                    value: product.category,
                    options: ["Formal Wear", "Casual Wear"],
                },
                {
                    name: "price",
                    label: "Price",
                    type: "number",
                    value: product.price,
                    step: "0.01",
                },
                {
                    name: "stock",
                    label: "Stock",
                    type: "number",
                    value: product.stock,
                },
            ]);
        }
    }

    editCustomer(id) {
        const customer = this.customers.find((c) => c.id === id);
        if (customer) {
            this.editingItem = id;
            this.editingType = "customer";
            this.openEditModal("Edit Customer", [
                {
                    name: "firstName",
                    label: "First Name",
                    type: "text",
                    value: customer.firstName,
                },
                {
                    name: "lastName",
                    label: "Last Name",
                    type: "text",
                    value: customer.lastName,
                },
                {
                    name: "email",
                    label: "Email",
                    type: "email",
                    value: customer.email,
                },
                {
                    name: "phone",
                    label: "Phone",
                    type: "tel",
                    value: customer.phone,
                },
            ]);
        }
    }

    editOrder(id) {
        const order = this.orders.find((o) => o.id === id);
        if (order) {
            this.editingItem = id;
            this.editingType = "order";
            this.openEditModal("Edit Order", [
                {
                    name: "status",
                    label: "Status",
                    type: "select",
                    value: order.status,
                    options: ["Pending", "Processing", "Shipped", "Completed"],
                },
                {
                    name: "total",
                    label: "Total",
                    type: "number",
                    value: order.total,
                    step: "0.01",
                },
                {
                    name: "items",
                    label: "Items",
                    type: "number",
                    value: order.items,
                },
            ]);
        }
    }

    async deleteProduct(id) {
        if (confirm("Are you sure you want to delete this product?")) {
            try {
                await this.deleteProduct(id);
                await this.refreshProducts();
                this.showNotification(
                    "Product deleted successfully!",
                    "success"
                );
            } catch (error) {
                this.showNotification("Failed to delete product", "error");
            }
        }
    }

    async deleteCustomer(id) {
        if (confirm("Are you sure you want to delete this customer?")) {
            try {
                await this.deleteCustomerAPI(id);
                await this.refreshCustomers();
                this.showNotification(
                    "Customer deleted successfully!",
                    "success"
                );
            } catch (error) {
                this.showNotification("Failed to delete customer", "error");
            }
        }
    }

    async deleteOrder(id) {
        if (confirm("Are you sure you want to delete this order?")) {
            try {
                await this.deleteOrderAPI(id);
                await this.refreshOrders();
                this.showNotification("Order deleted successfully!", "success");
            } catch (error) {
                this.showNotification("Failed to delete order", "error");
            }
        }
    }

    openEditModal(title, fields) {
        document.getElementById("modal-title").textContent = title;
        const fieldsContainer = document.getElementById("edit-form-fields");

        fieldsContainer.innerHTML = fields
            .map((field) => {
                if (field.type === "select") {
                    return `
                    <div class="form-group">
                        <label class="form-label">${field.label}</label>
                        <select name="${
                            field.name
                        }" class="form-input" required>
                            ${field.options
                                .map(
                                    (option) =>
                                        `<option value="${option}" ${
                                            option === field.value
                                                ? "selected"
                                                : ""
                                        }>${option}</option>`
                                )
                                .join("")}
                        </select>
                    </div>
                `;
                } else {
                    return `
                    <div class="form-group">
                        <label class="form-label">${field.label}</label>
                        <input type="${field.type}" name="${
                        field.name
                    }" class="form-input" 
                               value="${field.value}" ${
                        field.step ? `step="${field.step}"` : ""
                    } required>
                    </div>
                `;
                }
            })
            .join("");

        document.getElementById("edit-modal").style.display = "block";
    }

    closeModal() {
        document.getElementById("edit-modal").style.display = "none";
        this.editingItem = null;
        this.editingType = null;
    }

    clearProductForm() {
        document.getElementById("product-form").reset();
    }

    clearCustomerForm() {
        document.getElementById("customer-form").reset();
    }

    handleSearch(query) {
        if (!query) {
            this.showPage(this.currentPage);
            return;
        }

        query = query.toLowerCase();

        switch (this.currentPage) {
            case "products":
                this.filterProducts(query);
                break;
            case "customers":
                this.filterCustomers(query);
                break;
            case "orders":
                this.filterOrders(query);
                break;
        }
    }

    filterProducts(query) {
        const filteredProducts = this.products.filter(
            (product) =>
                product.name.toLowerCase().includes(query) ||
                product.category.toLowerCase().includes(query)
        );

        const tbody = document.getElementById("products-tbody");
        tbody.innerHTML = filteredProducts
            .map(
                (product) => `
            <tr>
                <td>${product.id}</td>
                <td>${product.name}</td>
                <td>${product.category}</td>
                <td>$${product.price.toFixed(2)}</td>
                <td>${product.stock}</td>
                <td>${product.salesCount || 0}</td>
                <td>$${(product.revenue || 0).toFixed(2)}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteProduct(${
                        product.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    filterCustomers(query) {
        const filteredCustomers = this.customers.filter(
            (customer) =>
                customer.firstName.toLowerCase().includes(query) ||
                customer.lastName.toLowerCase().includes(query) ||
                customer.email.toLowerCase().includes(query)
        );

        const tbody = document.getElementById("customers-tbody");
        tbody.innerHTML = filteredCustomers
            .map(
                (customer) => `
            <tr>
                <td>${customer.id}</td>
                <td>${customer.firstName} ${customer.lastName}</td>
                <td>${customer.email}</td>
                <td>${customer.phone}</td>
                <td>${customer.created}</td>
                <td>${customer.orders || 0}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteCustomer(${
                        customer.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    filterOrders(query) {
        const filteredOrders = this.orders.filter(
            (order) =>
                order.customerName.toLowerCase().includes(query) ||
                order.status.toLowerCase().includes(query)
        );

        const tbody = document.getElementById("orders-tbody");
        tbody.innerHTML = filteredOrders
            .map(
                (order) => `
            <tr>
                <td>${order.id}</td>
                <td>${order.customerName}</td>
                <td>${order.date}</td>
                <td><span class="status status-${order.status.toLowerCase()}">${
                    order.status
                }</span></td>
                <td>$${order.total.toFixed(2)}</td>
                <td>${order.items}</td>
                <td>
                    <button class="btn btn-sm btn-danger" onclick="crm.deleteOrder(${
                        order.id
                    })">Delete</button>
                </td>
            </tr>
        `
            )
            .join("");
    }

    showNotification(message, type = "success") {
        const notification = document.createElement("div");
        notification.className = `notification notification-${type}`;
        notification.textContent = message;
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            color: white;
            padding: 12px 24px;
            border-radius: 4px;
            z-index: 1000;
            animation: slideIn 0.3s ease;
            background: ${type === "error" ? "#f44336" : "#4CAF50"};
        `;

        document.body.appendChild(notification);

        setTimeout(() => {
            notification.remove();
        }, 3000);
    }
}

// Global functions for onclick handlers
function closeModal() {
    if (window.crm) {
        window.crm.closeModal();
    }
}

// Initialize the application when DOM is loaded
document.addEventListener("DOMContentLoaded", () => {
    window.crm = new ClothingCRM();
});

// Add CSS for loading state and animations
const style = document.createElement("style");
style.textContent = `
    @keyframes slideIn {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    .status {
        padding: 4px 8px;
        border-radius: 4px;
        font-size: 12px;
        font-weight: bold;
        text-transform: uppercase;
    }
    
    .status-pending { background: #ff9800; color: white; }
    .status-processing { background: #2196f3; color: white; }
    .status-shipped { background: #9c27b0; color: white; }
    .status-completed { background: #4caf50; color: white; }
    
    .btn-danger {
        background: #f44336;
        color: white;
    }
    
    .btn-sm {
        padding: 4px 8px;
        font-size: 12px;
        margin: 0 2px;
    }
    
    .notification {
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    }
    
    .notification-error {
        background: #f44336 !important;
    }
    
    .notification-success {
        background: #4CAF50 !important;
    }
    
    .loading {
        cursor: wait;
    }
    
    .loading * {
        pointer-events: none;
    }
    
    .loading::before {
        content: '';
        position: fixed;
        top: 50%;
        left: 50%;
        width: 50px;
        height: 50px;
        margin: -25px 0 0 -25px;
        border: 3px solid #f3f3f3;
        border-top: 3px solid #3498db;
        border-radius: 50%;
        animation: spin 1s linear infinite;
        z-index: 9999;
    }
    
    @keyframes spin {
        0% { transform: rotate(0deg); }
        100% { transform: rotate(360deg); }
    }
`;
document.head.appendChild(style);
