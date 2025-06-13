import { mount } from '@vue/test-utils'
import ContactList from '../ContactList.vue'
import { describe, it, expect, vi } from 'vitest'

vi.mock('@/services/api', () => ({
  default: {
    getContacts: vi.fn(() => Promise.resolve({ data: [] })),
    searchContacts: vi.fn(() => Promise.resolve({ data: [] })),
    deleteContact: vi.fn(() => Promise.resolve())
  }
}))

describe('ContactList', () => {
  it('renders properly', async () => {
    const wrapper = mount(ContactList, {
      global: {
        stubs: {
          'router-link': true // Stub router-link component
        }
      }
    })

    await wrapper.vm.$nextTick()

    expect(wrapper.text()).toContain('Contacts')
    expect(wrapper.find('[data-test="search-input"]').exists()).toBe(true)
    expect(wrapper.find('[data-test="add-contact-btn"]').exists()).toBe(true)
  })

  it('shows error message when error occurs', async () => {
    const testError = 'Test error message'
    const wrapper = mount(ContactList, {
      global: {
        stubs: {
          'router-link': true
        }
      },
      data() {
        return {
          error: testError,
          contacts: [],
          searchQuery: ''
        }
      }
    })

    await wrapper.vm.$nextTick()

    const errorElement = wrapper.find('[data-test="error-message"]')
    expect(errorElement.exists()).toBe(true)
    expect(errorElement.text()).toContain(testError)
  })
})
